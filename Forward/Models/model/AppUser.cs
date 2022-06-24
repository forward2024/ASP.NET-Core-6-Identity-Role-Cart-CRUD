using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Forward.Models.model
{
    public class AppUser : IdentityUser
    {
        
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
    }
}
