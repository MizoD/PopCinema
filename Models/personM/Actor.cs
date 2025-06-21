namespace PopCinema.Models.personM
{
    public class Actor: Person
    {
        public ICollection<ActorMovie> Movies { get; set; } = new List<ActorMovie>();
    }
}
