namespace PopCinema.ViewModels
{
    public class MoviesWithShowsVM
    {
        
        public List<Movie> Movies { get; set; } = null!;
        public List<ShowTime> ShowTimes { get; set; } = null!;
        public MoviesWithShowsVM(List<Movie> movies, List<ShowTime> showTimes)
        {
            Movies = movies;
            ShowTimes = showTimes;
        }
    }
}
