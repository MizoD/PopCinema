namespace PopCinema.Models.CinemaM
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public int DiscountPercentage { get; set; } 
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public bool IsActive { get; set; }
    }

}
