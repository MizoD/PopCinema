using Microsoft.EntityFrameworkCore;

namespace PopCinema.Models.CinemaM
{
    [PrimaryKey(nameof(ApplicationUserId), nameof(ShowTimeId))]
    public class Cart
    {
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser? ApplicationUser { get; set; }
        public int ShowTimeId { get; set; }
        public ShowTime? ShowTime { get; set; }
        public  int Count { get; set; }
    }
}
