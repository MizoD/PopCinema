using PopCinema.Models.personM;

namespace PopCinema.ViewModels
{
    public class ActorsWithDirectorsVM
    {
        public List<Director> Directors { get; set; } = new List<Director>();
        public List<Actor> Actors { get; set; } = new List<Actor>();

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
