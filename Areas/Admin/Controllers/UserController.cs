using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace PopCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<ActionResult> Index(int page = 1)
        {
            var users =await _userManager.Users.ToListAsync();
            const int pageSize = 9;
            int totalActors = users.Count();

            users = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            Dictionary<ApplicationUser, string> keyValuePairs = new();

            foreach (var item in users)
            {
                keyValuePairs.Add(item, String.Join(",", await _userManager.GetRolesAsync(item)));
            }
                
            UsersVM vm = new UsersVM { Users = keyValuePairs.ToDictionary(), CurrentPage = page, TotalPages = (int)Math.Ceiling((double)totalActors / pageSize) };
            return View(vm);
        }
        
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public async Task<ActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            var registerVM = user.Adapt<AdminRegisterVM>();

            if (user is not null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                registerVM.Roles = _roleManager.Roles.Select( e => new SelectListItem()
                {
                    Text = e.Name,
                    Value = e.Name,
                    Selected = userRoles.Contains(e.Name)
                }).ToList();
                return View(registerVM);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public async Task<ActionResult> EditAsync(AdminRegisterVM adminRegisterVM, List<string> roles)
        {
              if (!ModelState.IsValid)
                {
                    return View(adminRegisterVM);
                }

                var user = await _userManager.FindByIdAsync(adminRegisterVM.Id);

                if (user is not null)
                {
                    user.FirstName = adminRegisterVM.FirstName;
                    user.LastName = adminRegisterVM.LastName;
                    user.UserName = adminRegisterVM.UserName;
                    user.Email = adminRegisterVM.Email;
                    user.Address = adminRegisterVM.Address;
                    await _userManager.UpdateAsync(user);

                    var userRoles = await _userManager.GetRolesAsync(user);

                    await _userManager.RemoveFromRolesAsync(user, userRoles);

                    await _userManager.AddToRolesAsync(user, roles);
                    return RedirectToAction(nameof(Index));
                }
            return View(adminRegisterVM);
                
            
        }
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null) return NotFound();

            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> LockUnLock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is not null)
            {
                if (user.LockoutEnabled)
                {
                    user.LockoutEnabled = false;
                    user.LockoutEnd = DateTime.UtcNow.AddMonths(1);
                    TempData["success-notification"] = "Block User Successfully";
                }
                else
                {
                    user.LockoutEnabled = true;
                    user.LockoutEnd = null;
                    TempData["success-notification"] = "UnBlock User Successfully";
                }

                await _userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}
