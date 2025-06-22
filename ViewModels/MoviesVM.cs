namespace PopCinema.ViewModels
{
    public class MoviesVM
    {
        public List<Movie> Movies { get; set; } = new List<Movie>();
        public List<ShowTime> ShowTimes { get; set; } = new List<ShowTime>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        
    }
}

