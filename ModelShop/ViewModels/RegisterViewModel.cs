using System.ComponentModel.DataAnnotations;

namespace ModelShop.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname{ get; set; }

        [Required]
        public string Username { get; set; }    

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password do not match!")]
        public string ConfirmPassword { get; set; }
    }
}
