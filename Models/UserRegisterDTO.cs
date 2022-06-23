using System.ComponentModel.DataAnnotations;

namespace ContosoPizza.Models
{
    public class UserRegisterDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string ? FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string ? LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string ? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string ? Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        public string ? ConfirmPassword { get; set; }
    }
}
