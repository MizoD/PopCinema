using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopCinema.Utility;

namespace PopCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class DirectorController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index(int page = 1)
        {
            const int pageSize = 9;
            IQueryable<Director> directors = _context.Directors.Include(a => a.Movies);
            int totalDirectors = directors.Count();

            directors = directors.Skip((page - 1) * pageSize)
                .Take(pageSize);
            ActorsWithDirectorsVM vm = new ActorsWithDirectorsVM { Directors = directors.ToList(), CurrentPage = page, TotalPages = (int)Math.Ceiling((double)totalDirectors / pageSize) };
            return View(vm);
        }
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public async Task<IActionResult> Create(Director director, IFormFile photoFile)
        {
            if (photoFile != null && photoFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\admin", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photoFile.CopyToAsync(stream);
                }

                director.PhotoUrl = filePath;
            }

            if (ModelState.IsValid)
            {
                _context.Directors.Add(director);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(director);
        }
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public IActionResult Edit(int Id)
        {
            var director = _context.Directors.Find(Id);

            return View(director);
        }

        [HttpPost]
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public async Task<IActionResult> Edit(Director director, IFormFile photo)
        {
            ModelState.Remove("PhotoUrl");
            if (!ModelState.IsValid)
            {
                return View(director);
            }
            var directorDb = await _context.Directors.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == director.Id);

            if (directorDb is not null)
            {
                if (photo is not null && photo.Length > 0)
                {
                    // Save new Img in wwwroot
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\admin", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await photo.CopyToAsync(stream);
                    }

                    // Delete old Img in wwwroot
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", directorDb.PhotoUrl);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }

                    // Update Img in DB
                    director.PhotoUrl = filePath;
                }
                else
                {
                    director.PhotoUrl = directorDb.PhotoUrl;
                }

                _context.Directors.Update(director);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return NotFound();
        }
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public IActionResult Delete(int Id)
        {
            var director = _context.Directors.Include(a => a.Movies).FirstOrDefault(s => s.Id == Id);
            if (director is null) return NotFound();

            
            if (System.IO.File.Exists(director.PhotoUrl))
            {
                System.IO.File.Delete(director.PhotoUrl);
            }

            _context.Directors.Remove(director);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
