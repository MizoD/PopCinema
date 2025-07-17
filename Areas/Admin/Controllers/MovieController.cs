using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PopCinema.Utility;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PopCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
    public class MovieController : Controller
    {
        ApplicationDbContext _context = new();
        private IMovieRepository movieRepository;

        public MovieController( IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await movieRepository.GetAsync(include: m=> m.Include(m=> m.Actors).ThenInclude(a=> a.Actor)
                        .Include(m=> m.CinemaHall).Include(m=> m.Director).Include(m=> m.Genre));
            
            return View(movies);
        }
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public async Task<IActionResult> Create()
        {
            var directors = await movieRepository.GetDirectorsSelectListAsync();
            var actors = await movieRepository.GetActorsSelectListAsync();
            var cinemaHalls = await movieRepository.GetCinemasSelectListAsync();
            var genres = await movieRepository.GetGenresSelectListAsync();

            DirActCinGenAdminVM vm = new DirActCinGenAdminVM { Actors = actors, Directors = directors,
                                    CinemaHalls = cinemaHalls, Genres = genres };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
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
                var directors = await movieRepository.GetDirectorsSelectListAsync();
                var actors = await movieRepository.GetActorsSelectListAsync();
                var cinemaHalls = await movieRepository.GetCinemasSelectListAsync();
                var genres = await movieRepository.GetGenresSelectListAsync();


                DirActCinGenAdminVM vm = new DirActCinGenAdminVM
                {
                    Actors = actors,
                    Directors = directors,
                    CinemaHalls = cinemaHalls,
                    Genres = genres,
                    Movie = movie
                };
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

                movie.PosterUrl = filePath;
            }

            movie.Actors = SelectedActorIds.Select(actorId => new ActorMovie
            {
                ActorId = actorId,
                Movie = movie
            }).ToList();


            await movieRepository.CreateAsync(movie);

            TempData["success-notification"] = "Add Movie Successfully";

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public async Task<IActionResult> Edit(int Id)
        {
            var movie = await movieRepository.GetOneAsync(m=> m.Id == Id, include: m => m.Include(m => m.Actors).ThenInclude(am => am.Actor));

            if (movie == null) return NotFound();

            var directors = await movieRepository.GetDirectorsSelectListAsync();
            var cinemaHalls = await movieRepository.GetCinemasSelectListAsync();
            var genres = await movieRepository.GetGenresSelectListAsync();

            var actorIds = await movieRepository.GetActorIdsForMovieAsync(Id);

            var actors = await movieRepository.GetActorSelectListAsync(actorIds);

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
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public async Task<IActionResult> Edit(Movie movie, IFormFile poster, List<int> selectedActorIds)
        {
            ModelState.Remove("PosterUrl");
            if (!ModelState.IsValid)
            {
                var vm = new DirActCinGenAdminVM
                {
                    Movie = movie,
                    Directors = await movieRepository.GetDirectorsSelectListAsync(),
                    Actors = await movieRepository.GetActorSelectListAsync(selectedActorIds),
                    CinemaHalls = await movieRepository.GetCinemasSelectListAsync(),
                    Genres = await movieRepository.GetGenresSelectListAsync()
                };
                return View(vm);
            }

            var movieInDb = await movieRepository.GetOneAsync( m=> m.Id == movie.Id, include: m => m.Include(m => m.Actors));

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
                    
                    if (System.IO.File.Exists(movieInDb.PosterUrl))
                        System.IO.File.Delete(movieInDb.PosterUrl);
                }

                movieInDb.PosterUrl = filePath;
            }
            //legacy
            _context.MovieActors.RemoveRange(movieInDb.Actors);
            movieInDb.Actors = selectedActorIds.Select(actorId => new ActorMovie
            {
                ActorId = actorId,
                MovieId = movie.Id
            }).ToList();
            //legacy

            await movieRepository.CommitAsync();

            TempData["success-notification"] = "✅ Movie updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await movieRepository.GetOneAsync(m=> m.Id == id, include: m=> m.Include(m=> m.Actors).Include(m=> m.ShowTimes));

            //legacy
            if (movie is not null)
            {
                _context.MovieActors.RemoveRange(movie.Actors);

                if (movie.ShowTimes != null && movie.ShowTimes.Any())
                {
                    _context.ShowTimes.RemoveRange(movie.ShowTimes);
                }

                if (System.IO.File.Exists(movie.PosterUrl))
                {
                    System.IO.File.Delete(movie.PosterUrl);
                }

                _context.Movies.Remove(movie); 

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }


    }
}
