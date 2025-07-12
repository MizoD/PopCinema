using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PopCinema.Models.MovieM;
using PopCinema.Utility;

namespace PopCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ActorController : Controller
    {
        ApplicationDbContext _context = new();

        public IActionResult Index(int page = 1)
        {
            const int pageSize = 9;
            IQueryable<Actor> actors = _context.Actors.Include(a=> a.Movies).ThenInclude(am => am.Movie);
            int totalActors = actors.Count();

            actors = actors.Skip((page - 1) * pageSize)
                .Take(pageSize);
            ActorsWithDirectorsVM vm = new ActorsWithDirectorsVM { Actors = actors.ToList(), CurrentPage = page, TotalPages = (int)Math.Ceiling((double)totalActors / pageSize) };
            return View(vm);
        }
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public async Task<IActionResult> Create(Actor actor, IFormFile photoFile)
        {
            if (photoFile != null && photoFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\admin", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photoFile.CopyToAsync(stream);
                }

                actor.PhotoUrl = filePath;
            }

            if (ModelState.IsValid)
            {
                _context.Actors.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(actor);
        }
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public IActionResult Edit(int Id)
        {
            var actor = _context.Actors.Find(Id);

            return View(actor);
        }

        [HttpPost]
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public async Task<IActionResult> Edit(Actor actor, IFormFile photo)
        {
            ModelState.Remove("PhotoUrl");
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            var actorDb = await _context.Actors.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == actor.Id);

            if (actorDb is not null)
            {
                if (photo is not null && photo.Length > 0)
                {
                    
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\admin", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await photo.CopyToAsync(stream);
                    }

                   
                    
                    if (System.IO.File.Exists(actorDb.PhotoUrl))
                    {
                        System.IO.File.Delete(actorDb.PhotoUrl);
                    }

                    // Update Img in DB
                    actor.PhotoUrl = filePath;
                }
                else
                {
                    actor.PhotoUrl = actorDb.PhotoUrl;
                }

                _context.Actors.Update(actor);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public IActionResult Delete(int Id)
        {
            var actor = _context.Actors.Include(a=> a.Movies).FirstOrDefault(s => s.Id == Id);
            if (actor is null) return NotFound();

            
            if (System.IO.File.Exists(actor.PhotoUrl))
            {
                System.IO.File.Delete(actor.PhotoUrl);
            }

            _context.MovieActors.RemoveRange(actor.Movies);

            _context.Actors.Remove(actor);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
