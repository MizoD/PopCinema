namespace PopCinema.Models.CinemaM
{
    public class Seat
    {
        public int Id { get; set; }
        public char Row { get; set; }
        public int Number { get; set; }
        public int CinemaHallId { get; set; }
        public CinemaHall? CinemaHall { get; set; }


    }
}
