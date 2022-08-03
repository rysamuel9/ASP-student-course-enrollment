using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyASPProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "isEmailInUse", controller: "Account")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmation Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password Not Match!")]
        public string ConfirmPassword { get; set; }
    }
}
