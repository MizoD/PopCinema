using System.ComponentModel.DataAnnotations;

namespace PopCinema.Models.MovieM
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public string ReviewerName { get; set; } =  null!;
        [Required]
        public int Rating { get; set; } 

        public string Comment { get; set; } = string.Empty;
        public DateTime ReviewDate { get; set; } = DateTime.Now;
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
       
    }

}
