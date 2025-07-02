using PopCinema.Models.MovieM;
using System.ComponentModel.DataAnnotations;

namespace PopCinema.Models.CinemaM
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingTime { get; set; } = DateTime.Now;

        [Required]
        public int NumberOfSeats { get; set; }
        public decimal TotalPrice { get; set; }
        public int CinemaHallId { get; set; }
        public CinemaHall CinemaHall { get; set; } = null!;

        [Required]
        public int ShowTimeId { get; set; }
        public ShowTime ShowTime { get; set; } = null!;
        public int? PromotionId { get; set; }
        public Promotion? Promotion { get; set; }

    }
}
