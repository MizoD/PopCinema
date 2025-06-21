namespace PopCinema.Models.personM
{
    public class Person
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
    }
}
