using Microsoft.AspNetCore.Mvc;

namespace PopCinema.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context = new();
        public IViewComponentResult Invoke()
        {
            var featuredMovies = _context.Movies
                .OrderByDescending(m => m.ReleaseDate)
                .Take(6)
                .ToList();

            return View(featuredMovies);
        }
    }
}
