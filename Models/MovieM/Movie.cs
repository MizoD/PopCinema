using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PopCinema.Models.CinemaM;
using PopCinema.Models.personM;
using System.ComponentModel.DataAnnotations;

namespace PopCinema.Models.MovieM
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Title { get; set; } = string.Empty;
        [MinLength(50), MaxLength(300)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(30,240)]
        public int DurationMinutes { get; set; }
        [Required]
        [Url]
        public string TrailerUrl { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public string Language { get; set; } = string.Empty;
        public int? Traffic { get; set; } = 0;
        [Required]
        public int DirectorId { get; set; }
        [ValidateNever]
        public Director Director { get; set; } = null!;
        [Required]
        public int GenreId { get; set; }
        [ValidateNever]
        public Genre Genre { get; set; } = null!;

        public int CinemaHallId { get; set; }
        [ValidateNever]
        public CinemaHall CinemaHall { get; set; } = null!;

        public List<int> ActorIds { get; set; } = new();
        public ICollection<ActorMovie> Actors { get; set; } = new List<ActorMovie>();
        public ICollection<ShowTime> ShowTimes { get; set; } = new List<ShowTime>();
    }
}
