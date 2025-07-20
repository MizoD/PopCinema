
using Microsoft.EntityFrameworkCore;

namespace PopCinema.Models.CinemaM
{
    [PrimaryKey(nameof(BookingId), nameof(ShowTimeId))]
    public class BookedItem
    {
        public int BookingId { get; set; }
        public Booking Booking { get; set; } = null!;
        public int ShowTimeId { get; set; }
        public ShowTime ShowTime { get; set; } = null!;

        public decimal TotalPrice { get; set; }
    }
}
