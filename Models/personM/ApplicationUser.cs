using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PopCinema.Models.personM
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
    }
}
