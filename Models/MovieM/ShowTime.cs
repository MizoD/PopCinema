using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PopCinema.Models.CinemaM;
using System.ComponentModel.DataAnnotations;

namespace PopCinema.Models.MovieM
{
    public class ShowTime
    {
        public int Id { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Ticket price must be greater than 0.")]
        public decimal TicketPrice { get; set; }
        [Required]
        public int MovieId { get; set; }
        [ValidateNever]
        public Movie Movie { get; set; } = null!;
        [Required]
        public int CinemaHallId { get; set; }
        [ValidateNever]
        public CinemaHall CinemaHall { get; set; } = null!;

        public ICollection<Booking>? Bookings { get; set; }


    }
}
