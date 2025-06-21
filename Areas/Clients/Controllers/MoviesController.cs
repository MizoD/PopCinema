using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PopCinema.Areas.Clients.Controllers
{
    [Area("Clients")]
    public class MoviesController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index(int moviePage = 1, int showPage = 1)
        {
            //const double totalNumberInPages = 8.0;
            IQueryable<Movie> movies = _context.Movies.Include(e=> e.Actors).ThenInclude(e=> e.Actor).Include(e=> e.ShowTimes);
            IQueryable<ShowTime> showTimes = _context.ShowTimes.Include(e=> e.CinemaHall).OrderBy(e => e.StartTime).Skip(0).Take(8);
            

            //var totalNumberOfMoviePages = Math.Ceiling(movies.Count() / totalNumberInPages);
            //var totalNumberOfShowPages = Math.Ceiling(showTimes.Count() / totalNumberInPages);

            //if (totalNumberOfMoviePages < moviePage || totalNumberOfShowPages < showPage)
            //    return RedirectToAction(nameof(NotFoundPage));

            //movies = movies.Skip((moviePage - 1) * (int)totalNumberInPages).Take((int)totalNumberInPages);

            //ViewBag.totalNumberOfMoviePages = totalNumberOfMoviePages;
            //ViewBag.currentMoviePage = moviePage;

            //showTimes = showTimes.Skip((showPage - 1) * (int)totalNumberInPages).Take((int)totalNumberInPages);

            //ViewBag.totalNumberOfShowPages = totalNumberOfShowPages;
            //ViewBag.currentShowPage = showPage;

            MoviesWithShowsVM moviesWithShows = new(movies.ToList(), showTimes.ToList());
            return View(moviesWithShows);
        }
        public IActionResult Details(int id)
        {
            var movie = _context.Movies.Include(e=> e.CinemaHall).Include(e=> e.Director).Include(e=> e.Genre).Include(e=> e.ShowTimes).Include(e => e.Actors).ThenInclude(e => e.Actor)
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }

            return View(movie);
        }

        [HttpGet]
        public IActionResult Search(string query)
        {
            if (string.IsNullOrEmpty(query))
                return RedirectToAction("Index", "Home");

            var movie = _context.Movies
                .FirstOrDefault(m => m.Title.ToLower().Contains(query.ToLower()));

            if (movie == null)
                return RedirectToAction(nameof(NotFoundPage));

            return RedirectToAction("Details", new { id = movie.Id });
        }

        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
