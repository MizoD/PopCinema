using Microsoft.AspNetCore.Mvc.Rendering;

namespace PopCinema.ViewModels
{
    public class DirActCinGenAdminVM
    {
        public Movie? Movie { get; set; }
        public IEnumerable<SelectListItem> Directors { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Actors { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> CinemaHalls { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Genres { get; set; } = new List<SelectListItem>();
        public List<int> SelectedActorIds { get; set; } = new List<int>();
    }
}
