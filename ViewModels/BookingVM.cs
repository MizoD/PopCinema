namespace PopCinema.ViewModels
{
    public class BookingVM
    {
        public int ShowTimeId { get; set; }
        public List<ShowTime> ShowTimes { get; set; } = new List<ShowTime>();
        public string MovieTitle { get; set; } = string.Empty;
        public int CinemaHallId { get; set; }
        public int AvailableSeats { get; set; }
        public decimal TicketPrice { get; set; }

    }
}
