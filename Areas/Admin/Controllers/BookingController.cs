using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PopCinema.Models.personM;
using PopCinema.Utility;
using System.Threading.Tasks;

namespace PopCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class BookingController : Controller
    {
        private ApplicationDbContext _context = new();
        private readonly IBookingRepository bookingRepository;

        public BookingController(ApplicationDbContext context, IBookingRepository bookingRepository)
        { 
            this.bookingRepository = bookingRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 9;
            var bookings = await bookingRepository.GetAsync(include: b => b.Include(b => b.CinemaHall).Include(b => b.ShowTime).ThenInclude(s => s.Movie).Include(b => b.Promotion));
            
            int totalBookings = bookings.Count();
            bookings = bookings.Skip((page - 1) * pageSize)
                .Take(pageSize);
            BookingPaginationVM vm = new BookingPaginationVM { Bookings = bookings.ToList(), CurrentPage = page, TotalPages = (int)Math.Ceiling((double)totalBookings / pageSize) };
            return View(vm);
        }
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public async Task<IActionResult> Save(int Id)
        {
            var booking = await bookingRepository.GetOneAsync(b => b.Id == Id);
            //legacy    
            var showtimes = _context.ShowTimes.Include(s=> s.Movie).Where(e => e.EndTime > DateTime.Now).OrderBy(st => st.StartTime).ToList();
            var showtimeOptions = showtimes.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = $"{s.Movie.Title} - {s.StartTime:f} – ${s.TicketPrice}"
            }).ToList();
            BookingAdminVM vm;
            if (booking is null)
            {
                vm = new BookingAdminVM { ShowTimes = showtimeOptions };
                return View(vm);
            }
            vm = new BookingAdminVM { Booking = booking, ShowTimes = showtimeOptions};

            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public async Task<IActionResult> Save(Booking booking)
        {
            ModelState.Remove("Booking.CinemaHall");
            ModelState.Remove("Booking.ShowTime");

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var showtimes = _context.ShowTimes
                    .Include(s => s.Movie)
                    .Where(e => e.EndTime > DateTime.Now)
                    .OrderBy(st => st.StartTime)
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = $"{s.Movie.Title} - {s.StartTime:f} – ${s.TicketPrice}"
                    }).ToList();

                var vm = new BookingAdminVM { Booking = booking, ShowTimes = showtimes };
                return View(vm);
            }

            var showtime = _context.ShowTimes.Find(booking.ShowTimeId);
            if (showtime == null)
                return NotFound();

            booking.CinemaHallId = showtime.CinemaHallId;
            booking.PromotionId = 1;
            booking.TotalPrice = 0;

            var existing = await bookingRepository.GetOneAsync(b => b.Id == booking.Id);

            if (existing == null)
                await bookingRepository.CreateAsync(booking);
            else
                await bookingRepository.UpdateAsync(booking);


            return RedirectToAction("Index");
        }

        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var booking = await bookingRepository.GetOneAsync(b => b.Id == Id);
            if (booking is null) return NotFound();

            await bookingRepository.DeleteAsync(booking);
         
            return RedirectToAction("Index");
        }
    }
}
