using Microsoft.AspNetCore.Mvc;

namespace PopCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShowTimeController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            var showtimes = _context.ShowTimes;
            return View(showtimes);
        }
    }
}
