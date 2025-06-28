namespace PopCinema.ViewModels
{
    public class CountAll
    {
        public int ActorsCount { get; set; }
        public int DirectorsCount { get; set; }
        public int MoviesCount { get; set; }
        public int ShowTimesCount { get; set; }
        public decimal TotalSales { get; set; }
        public Booking Booking { get; set; } = new Booking { };
        public List<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
