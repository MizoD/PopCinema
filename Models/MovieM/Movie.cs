using PopCinema.Models.CinemaM;
using PopCinema.Models.personM;

namespace PopCinema.Models.MovieM
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; 
        public int DurationMinutes { get; set; }
        public string TrailerUrl { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string Language { get; set; } = string.Empty;
        public int? Traffic { get; set; } = 0;
        public int DirectorId { get; set; }
        public Director Director { get; set; } = null!;

        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
        public int CinemaHallId { get; set; }
        public CinemaHall CinemaHall { get; set; } = null!;

        public ICollection<ActorMovie> Actors { get; set; } = new List<ActorMovie>();
        public ICollection<ShowTime> ShowTimes { get; set; } = new List<ShowTime>();
    }
}
