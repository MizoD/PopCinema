using Microsoft.AspNetCore.Mvc;

namespace PopCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            var movies = _context.Movies;
            return View(movies);
        }
    }
}
