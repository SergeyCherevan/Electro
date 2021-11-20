using System.ComponentModel.DataAnnotations;

namespace Electro.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "UserName is not specified")]
        [UIHint("UserName")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is not specified")]
        [UIHint("Password")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
