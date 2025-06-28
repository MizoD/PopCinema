using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using PopCinema.Models.MovieM;

namespace PopCinema.Models.personM
{
    public class Director : Person
    {
        [ValidateNever]
        public ICollection<Movie>? Movies { get; set; }
    }
}
