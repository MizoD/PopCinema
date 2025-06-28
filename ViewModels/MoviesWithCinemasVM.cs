using Microsoft.AspNetCore.Mvc.Rendering;

namespace PopCinema.ViewModels
{
    public class MoviesWithCinemasVM
    {
        public IEnumerable<SelectListItem> Movies { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> CinemaHalls { get; set; } = new List<SelectListItem>();
        public ShowTime ShowTime { get; set; } = new();
    }
}
