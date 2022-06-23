using System.ComponentModel.DataAnnotations;
namespace ContosoPizza.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string ? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string ? Password { get; set; }
        public bool ? Active { get; set; }
    }
}
