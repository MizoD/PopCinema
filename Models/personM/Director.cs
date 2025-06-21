using PopCinema.Models.MovieM;

namespace PopCinema.Models.personM
{
    public class Director : Person
    {
        public ICollection<Movie>? Movies { get; set; }
    }
}
