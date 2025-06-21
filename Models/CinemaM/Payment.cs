namespace PopCinema.Models.CinemaM
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string PaymentMethod { get; set; } = string.Empty;
        public string TransactionId { get; set; } = GenerateTransactionId();
        public bool IsSuccessful { get; set; }

        public int BookingId { get; set; }
        public Booking? Booking { get; set; }

        private static string GenerateTransactionId()
        {
            var datePart = DateTime.Now.ToString("yyyyMMdd");
            var randomPart = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
            return $"TXN-{datePart}-{randomPart}";
        }

    }

}
