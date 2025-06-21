namespace PopCinema.ViewModels
{
    public class BookingConfirmationViewModel
    {
        public string MovieTitle { get; set; } = string.Empty;
        public DateTime Showtime { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal TicketPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
        public int RemainingSeats { get; set; }
        public string? PromotionCode { get; set; }
    }
}
