using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace PopCinema.Models.personM
{
    public class Actor: Person
    {
        [ValidateNever]
        public ICollection<ActorMovie> Movies { get; set; } = new List<ActorMovie>();
    }
}
