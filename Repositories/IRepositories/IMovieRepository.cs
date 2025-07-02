using Microsoft.AspNetCore.Mvc.Rendering;

namespace PopCinema.Repositories.IRepositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<SelectListItem>> GetActorsSelectListAsync();

        Task<IEnumerable<SelectListItem>> GetDirectorsSelectListAsync();

        Task<IEnumerable<SelectListItem>> GetCinemasSelectListAsync();

        Task<IEnumerable<SelectListItem>> GetGenresSelectListAsync();
        Task<List<int>> GetActorIdsForMovieAsync(int movieId);
        Task<List<SelectListItem>> GetActorSelectListAsync(List<int>? selectedActorIds = null);
    }
}
