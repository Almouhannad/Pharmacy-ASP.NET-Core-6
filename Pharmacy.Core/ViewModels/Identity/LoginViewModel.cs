using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Identity
{
    public class LoginViewModel
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage ="Email address is required")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; } = null;
    }
}
