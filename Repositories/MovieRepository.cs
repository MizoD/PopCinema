using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace PopCinema.Repositories
{
    public class MovieRepository : Repository<Movie> , IMovieRepository
    {
        public MovieRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<SelectListItem>> GetDirectorsSelectListAsync()
        {
            return await _context.Directors
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.FullName
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<SelectListItem>> GetActorsSelectListAsync()
        {
            return await _context.Actors
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FullName
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<SelectListItem>> GetCinemasSelectListAsync()
        {
            return await _context.CinemaHalls
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();
        }
        public async Task<IEnumerable<SelectListItem>> GetGenresSelectListAsync()
        {
            return await _context.Genres
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                })
                .ToListAsync();
        }
        public async Task<List<int>> GetActorIdsForMovieAsync(int movieId)
        {
            return await _context.MovieActors
                .Where(ma => ma.MovieId == movieId)
                .Select(ma => ma.ActorId)
                .ToListAsync();
        }
        public async Task<List<SelectListItem>> GetActorSelectListAsync(List<int>? selectedActorIds = null)
        {
            selectedActorIds ??= new List<int>();

            return await _context.Actors
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.FullName,
                    Selected = selectedActorIds.Contains(a.Id)
                })
                .ToListAsync();
        }


    }
}
