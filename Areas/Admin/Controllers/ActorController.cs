using Microsoft.AspNetCore.Mvc;

namespace PopCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ActorController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            var actors = _context.Actors;
            
            return View(actors);
        }
    }
}
