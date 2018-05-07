using Projekt.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt.View_Models
{
    public class ManageCredentialsViewModel
    {
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
        public Projekt.Controllers.ManageController.ManageMessageId? Message { get; set; }
        public DaneUzytkownika DaneUzytkownika { get; set; }
    }

    public class ChangePasswordViewModel
    {

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Current Password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage ="Błędne hasło")]
        public string ConfirmPassword { get; set; }
    }



}