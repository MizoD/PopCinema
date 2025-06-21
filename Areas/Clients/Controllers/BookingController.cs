using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopCinema.Models.MovieM;

namespace PopCinema.Areas.Clients.Controllers
{
    [Area("Clients")]
    public class BookingController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index(int id)
        {
            var movie = _context.Movies
                .Include(m => m.ShowTimes)
                .ThenInclude(st => st.CinemaHall)
                .FirstOrDefault(m => m.Id == id);

            if (movie == null) return RedirectToAction(nameof(NotFoundPage));

            var defaultShowtime = movie.ShowTimes
                .Where(st => st.StartTime > DateTime.Now)
                .OrderBy(st => st.StartTime)
                .FirstOrDefault();

            if (defaultShowtime == null) return RedirectToAction(nameof(NotFoundPage));

            var bookedSeats = _context.Bookings
                .Where(b => b.ShowTimeId == defaultShowtime.Id)
                .Sum(b => b.NumberOfSeats);

            var vm = new BookingVM
            {
                ShowTimes = movie.ShowTimes.OrderBy(st => st.StartTime).ToList(),
                ShowTimeId = defaultShowtime.Id,
                MovieTitle = movie.Title,
                CinemaHallId = defaultShowtime.CinemaHallId,
                AvailableSeats = defaultShowtime.CinemaHall.Capacity - bookedSeats,
                TicketPrice = defaultShowtime.TicketPrice
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult Index(int ShowTimeId, int NumberOfSeats, string? PromoCode)
        {
            var showtime = _context.ShowTimes
                .Include(s => s.Movie)
                .Include(s => s.CinemaHall)
                .Include(s => s.Movie.ShowTimes)
                .FirstOrDefault(s => s.Id == ShowTimeId);

            if (showtime is null) return RedirectToAction(nameof(NotFoundPage));

            var bookedSeats = _context.Bookings
                .Where(b => b.ShowTimeId == ShowTimeId)
                .Sum(b => b.NumberOfSeats);

            var availableSeats = showtime.CinemaHall.Capacity - bookedSeats;
            if (NumberOfSeats > availableSeats)
            {
                var vm = new BookingVM
                {
                    ShowTimeId = ShowTimeId,
                    ShowTimes = showtime.Movie.ShowTimes.OrderBy(st => st.StartTime).ToList(),
                    MovieTitle = showtime.Movie.Title,
                    CinemaHallId = showtime.CinemaHallId,
                    AvailableSeats = availableSeats,
                    TicketPrice = showtime.TicketPrice
                };

                ModelState.AddModelError("", $"Only {availableSeats} seats are available.");
                return View(vm);
            }

            var promotion = _context.Promotions.FirstOrDefault(p =>
                p.Code == PromoCode &&
                p.IsActive &&
                DateTime.Now >= p.ValidFrom &&
                DateTime.Now <= p.ValidTo);

            decimal ticketTotal = showtime.TicketPrice * NumberOfSeats;
            decimal discount = promotion != null ? ticketTotal * (promotion.DiscountPercentage / 100) : 0;
            decimal totalAmount = ticketTotal - discount;

            var booking = new Booking
            {
                ShowTimeId = ShowTimeId,
                CinemaHallId = showtime.CinemaHallId,
                NumberOfSeats = NumberOfSeats,
                BookingTime = DateTime.Now,
                PromotionId = promotion?.Id,
                TotalPrice = totalAmount
            };

            showtime.Movie.Traffic++;
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            var updatedBookedSeats = _context.Bookings
                .Where(b => b.ShowTimeId == ShowTimeId)
                .Sum(b => b.NumberOfSeats);
            var updatedAvailableSeats = showtime.CinemaHall.Capacity - updatedBookedSeats;

            return View("Confirmation", new BookingConfirmationViewModel
            {
                MovieTitle = showtime.Movie.Title,
                Showtime = showtime.StartTime,
                NumberOfSeats = NumberOfSeats,
                TicketPrice = showtime.TicketPrice,
                Discount = discount,
                TotalPrice = totalAmount,
                RemainingSeats = updatedAvailableSeats,
                PromotionCode = PromoCode
            });
        }

        [HttpGet]
        public IActionResult GetShowtimeDetails(int id)
        {
            var showtime = _context.ShowTimes
                .Include(s => s.CinemaHall)
                .FirstOrDefault(s => s.Id == id);

            if (showtime == null)
                return NotFound();

            var booked = _context.Bookings
                .Where(b => b.ShowTimeId == id)
                .Sum(b => b.NumberOfSeats);

            return Json(new
            {
                availableSeats = showtime.CinemaHall.Capacity - booked,
                ticketPrice = showtime.TicketPrice
            });
        }

        public IActionResult Booked(DateTime? date, string? search)
        {
            var bookings = _context.Bookings
                .Include(b => b.ShowTime)
                    .ThenInclude(st => st.Movie)
                .Include(b => b.CinemaHall)
                .Include(b => b.Promotion)
                .AsQueryable();

            if (date.HasValue)
            {
                bookings = bookings.Where(b => b.ShowTime.StartTime.Date == date.Value.Date);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                bookings = bookings.Where(b => b.ShowTime.Movie.Title.Contains(search));
            }

            ViewBag.Date = date?.ToString("yyyy-MM-dd");
            ViewBag.Search = search;

            return View(bookings.ToList());
        }

        public IActionResult Confirmation()
        {
            return View();
        }
        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
