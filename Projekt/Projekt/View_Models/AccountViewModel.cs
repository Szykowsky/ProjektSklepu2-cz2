using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt.View_Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Wprowadz e-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Wprowadz hasło")]
        [DataType(DataType.Password)]
        [Display(Name ="Hasło")]
        public string Password { get; set; }

        [Display(Name ="Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wprowadz hasło")]
        [StringLength(30,ErrorMessage ="{0} musi mieć co najmniej {2} znaków",MinimumLength =6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdz Hasło")]
        [Compare("Password",ErrorMessage ="Hasła muszą być takie same") ]
        public string ConfirmPassword { get; set; }
    }
}