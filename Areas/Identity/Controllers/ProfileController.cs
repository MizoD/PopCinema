using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PopCinema.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [Route("Identity/Profile/Index/{userId}")]
        public async Task<IActionResult> Index(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return NotFound();
            return View(user);
        }

        public async Task<IActionResult> Edit(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user is null) return NotFound();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            var userDb = await _userManager.FindByIdAsync(user.Id);
            if (user is null) return NotFound();
            return View("Index");
        }
    }
}
