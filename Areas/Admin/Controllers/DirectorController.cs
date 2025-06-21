using Microsoft.AspNetCore.Mvc;

namespace PopCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DirectorController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            var directors = _context.Directors;

            return View(directors);
        }
    }
}
