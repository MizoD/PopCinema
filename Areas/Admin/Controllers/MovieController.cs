using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace PopCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index()
        {
            var movies = _context.Movies.Include(e => e.Actors).ThenInclude(e => e.Actor)
                .Include(e => e.CinemaHall).Include(e => e.Director).Include(e => e.Genre);
            return View(movies);
        }

        public IActionResult Create()
        {
            var directors = _context.Directors.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.FullName
            }).ToList(); ;
            var actors = _context.Actors.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.FullName
            }).ToList();
            var cinemaHalls = _context.CinemaHalls.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToList();
            var genres = _context.Genres.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            }).ToList();

            DirActCinGenAdminVM vm = new DirActCinGenAdminVM { Actors = actors, Directors = directors, CinemaHalls = cinemaHalls, Genres = genres };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movie movie, IFormFile poster, List<int> SelectedActorIds)
        {
            
            ModelState.Remove("PosterUrl");
            if (!ModelState.IsValid)
            {

                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.Errors.Any())
                    {
                        Console.WriteLine($"Key: {key}, Error: {state.Errors.First().ErrorMessage}");
                    }
                }
                var directors = _context.Directors.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.FullName
                }).ToList(); ;
                var actors = _context.Actors.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.FullName
                }).ToList();
                var cinemaHalls = _context.CinemaHalls.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToList();
                var genres = _context.Genres.Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name
                }).ToList();

                DirActCinGenAdminVM vm = new DirActCinGenAdminVM { Actors = actors, Directors = directors, CinemaHalls = cinemaHalls, Genres = genres, Movie = movie };
                return View(vm);
            }


            if (poster is not null && poster.Length > 0)
            {
               
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(poster.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\admin", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await poster.CopyToAsync(stream);
                }

                movie.PosterUrl = "/assets/admin/" + fileName;
            }

            movie.Actors = SelectedActorIds.Select(actorId => new ActorMovie
            {
                ActorId = actorId,
                Movie = movie
            }).ToList();


            _context.Movies.Add(movie);
            _context.SaveChanges();

            TempData["success-notification"] = "Add Movie Successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int Id)
        {
            var movie = _context.Movies
                .Include(m => m.Actors)
                .ThenInclude(ma => ma.Actor)
                .FirstOrDefault(m => m.Id == Id);

            if (movie == null) return NotFound();

            var directors = _context.Directors.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.FullName
            }).ToList();

            var genres = _context.Genres.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();

            var cinemaHalls = _context.CinemaHalls.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            var actorIds = movie.Actors.Select(ma => ma.ActorId).ToList();

            var actors = _context.Actors
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FullName,
                    Selected = actorIds.Contains(a.Id)
                })
                .ToList();

            var vm = new DirActCinGenAdminVM
            {
                Movie = movie,
                Directors = directors,
                Genres = genres,
                CinemaHalls = cinemaHalls,
                Actors = actors
            };

            return View(vm);
        }
    

        [HttpPost]
        public async Task<IActionResult> Edit(Movie movie, IFormFile poster, List<int> SelectedActorIds)
        {
            ModelState.Remove("PosterUrl");
            if (!ModelState.IsValid)
            {
                var vm = new DirActCinGenAdminVM
                {
                    Movie = movie,
                    Directors = _context.Directors.Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.FullName }).ToList(),
                    Actors = _context.Actors.Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.FullName,
                        Selected = SelectedActorIds.Contains(a.Id)
                    }).ToList(),
                    CinemaHalls = _context.CinemaHalls.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList(),
                    Genres = _context.Genres.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name }).ToList()
                };
                return View(vm);
            }

            var movieInDb = await _context.Movies
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Id == movie.Id);

            if (movieInDb is null)
                return NotFound();

            movieInDb.Title = movie.Title;
            movieInDb.Description = movie.Description;
            movieInDb.DurationMinutes = movie.DurationMinutes;
            movieInDb.TrailerUrl = movie.TrailerUrl;
            movieInDb.ReleaseDate = movie.ReleaseDate;
            movieInDb.Language = movie.Language;
            movieInDb.DirectorId = movie.DirectorId;
            movieInDb.GenreId = movie.GenreId;
            movieInDb.CinemaHallId = movie.CinemaHallId;

            if (poster != null && poster.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(poster.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\admin", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await poster.CopyToAsync(stream);
                }

                if (!string.IsNullOrEmpty(movieInDb.PosterUrl))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", movieInDb.PosterUrl);
                    if (System.IO.File.Exists(oldFilePath))
                        System.IO.File.Delete(oldFilePath);
                }

                movieInDb.PosterUrl = "/assets/admin/" + fileName;
            }

            _context.MovieActors.RemoveRange(movieInDb.Actors);
            movieInDb.Actors = SelectedActorIds.Select(actorId => new ActorMovie
            {
                ActorId = actorId,
                MovieId = movie.Id
            }).ToList();

            await _context.SaveChangesAsync();

            TempData["success-notification"] = "✅ Movie updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Actors)
                .Include(m => m.ShowTimes) 
                .FirstOrDefault(m => m.Id == id);

            if (movie is not null)
            {
                _context.MovieActors.RemoveRange(movie.Actors);

                if (movie.ShowTimes != null && movie.ShowTimes.Any())
                {
                    _context.ShowTimes.RemoveRange(movie.ShowTimes);
                }

                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\admin", movie.PosterUrl);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                _context.Movies.Remove(movie);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }


    }
}
