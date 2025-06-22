using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PopCinema.Areas.Clients.Controllers
{
    [Area("Clients")]
    public class MoviesController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index(int page = 1)
        {
            const int pageSize = 8;

            IQueryable<Movie> movies = _context.Movies.Include(e => e.ShowTimes).Include(e=> e.Actors).ThenInclude(e=> e.Actor);
            IQueryable<ShowTime> showTimes = _context.ShowTimes.Where(e=> e.EndTime > DateTime.Now).Include(s => s.Movie).ThenInclude(m => m.ShowTimes).Include(e=> e.CinemaHall).OrderBy(e => e.StartTime).Skip(0).Take(8);

            int totalMovies = movies.Count();

            movies = movies
                .OrderBy(m => m.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
            var vm = new MoviesVM
            {
                Movies = movies.ToList(),
                ShowTimes = showTimes.ToList(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalMovies / pageSize)
            };
            return View(vm);
        }
        public IActionResult Details(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .Include(m => m.CinemaHall)
                .Include(m => m.ShowTimes)
                    .ThenInclude(s => s.CinemaHall)
                .Include(m => m.Actors)
                    .ThenInclude(am => am.Actor)
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }
            var reviews = _context.Reviews
                .Where(r => r.MovieId == id)
                .OrderByDescending(r => r.ReviewDate)
                .ToList();

            ViewBag.Reviews = reviews;


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

        [HttpPost]
        public IActionResult AddReview(Review review)
        {
            Console.WriteLine($"Review Data: {review.ReviewerName}, {review.Comment}, {review.Rating}, {review.MovieId}");

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill in all fields correctly.";
                return RedirectToAction("Details", new { id = review.MovieId });
            }

            var movie = _context.Movies.Find(review.MovieId);
            if (movie == null) return NotFound();

            review.ReviewDate = DateTime.Now;
            _context.Reviews.Add(review);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = review.MovieId });
        }

        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
