namespace PopCinema.ViewModels
{
    public class BookingPaginationVM
    {
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
