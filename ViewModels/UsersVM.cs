using PopCinema.Models.personM;

namespace PopCinema.ViewModels
{
    public class UsersVM
    {
        public Dictionary<ApplicationUser, string> Users { get; set; } = new Dictionary<ApplicationUser, string>();

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
