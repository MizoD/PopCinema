using PopCinema.Models.MovieM;

namespace PopCinema.Models.CinemaM
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingTime { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal TotalPrice { get; set; }
        public int CinemaHallId { get; set; }
        public CinemaHall CinemaHall { get; set; } = null!;

        public int ShowTimeId { get; set; }
        public ShowTime ShowTime { get; set; } = null!;
        public int? PromotionId { get; set; }
        public Promotion? Promotion { get; set; }

        public decimal TotalAmount
        {
            get
            {
                decimal ticketTotal = ShowTime?.TicketPrice * NumberOfSeats ?? 0;

                decimal discount = 0;
                if (Promotion != null && Promotion.IsActive && DateTime.Now >= Promotion.ValidFrom && DateTime.Now <= Promotion.ValidTo)
                {
                    discount = ticketTotal * (Promotion.DiscountPercentage / 100);
                }

                return ticketTotal - discount;
            }
        }

    }
}
