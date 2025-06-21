using PopCinema.Models.MovieM;

namespace PopCinema.Models.CinemaM
{
    public class CinemaHall
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
        public ICollection<Movie>? Movies { get; set; }
        public ICollection<ShowTime>? ShowTimes { get; set; }
        public ICollection<Seat>? Seats { get; set; }
    }
}
