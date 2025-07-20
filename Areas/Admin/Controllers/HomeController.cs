using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PopCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
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
                TotalSales = _context.Bookings.Sum(b => b.TotalPrice),
                
            };

            return View(count);
        }
    }
}
