using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce514.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            CountAll count = new CountAll
            {
                ActorsCount = _context.Actors.Count(),
                MoviesCount = _context.Movies.Count(),
                ShowTimesCount = _context.ShowTimes.Where(e => e.EndTime > DateTime.Now).Count(),
                DirectorsCount = _context.Directors.Count(),
                Bookings = _context.Bookings.Include(e=> e.CinemaHall).Include(e=> e.ShowTime).Include(e=> e.Promotion).ToList()
            };

            return View(count);
        }
    }
}
