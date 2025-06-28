using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopCinema.Models.personM;

namespace PopCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index(int page = 1)
        {
            int pageSize = 9;
            IQueryable<Booking> bookings = _context.Bookings.Include(e=> e.CinemaHall).Include(e=> e.ShowTime).ThenInclude(s=> s.Movie).Include(e=> e.Promotion);
            
            int totalActors = bookings.Count();
            bookings = bookings.Skip((page - 1) * pageSize)
                .Take(pageSize);
            BookingPaginationVM vm = new BookingPaginationVM { Bookings = bookings.ToList(), CurrentPage = page, TotalPages = (int)Math.Ceiling((double)totalActors / pageSize) };
            return View(vm);
        }
    }
}
