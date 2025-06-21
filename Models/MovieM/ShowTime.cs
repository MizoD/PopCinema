using PopCinema.Models.CinemaM;

namespace PopCinema.Models.MovieM
{
    public class ShowTime
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal TicketPrice { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public int CinemaHallId { get; set; }
        public CinemaHall CinemaHall { get; set; } = null!;

        public ICollection<Booking>? Bookings { get; set; }


    }
}
