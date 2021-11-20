using System.ComponentModel.DataAnnotations;

namespace Electro.Models.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Email is not specified")]
        [UIHint("Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [UIHint("Full name")]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "UserName is not specified")]
        [UIHint("UserName")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is not specified")]
        [UIHint("Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Confirm the entered password")]
        [UIHint("Password")]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }
    }
}
