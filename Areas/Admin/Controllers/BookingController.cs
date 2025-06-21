using Microsoft.AspNetCore.Mvc;

namespace PopCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            var bookings = _context.Bookings;
            return View(bookings);
        }
    }
}
