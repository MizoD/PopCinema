namespace PopCinema.ViewModels
{
    public class ShowTimesVM
    {
        public List<ShowTime> ShowTimes { get; set; } = new List<ShowTime>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
