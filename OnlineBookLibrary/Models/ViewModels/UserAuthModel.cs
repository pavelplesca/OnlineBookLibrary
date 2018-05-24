using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBookLibrary.Models
{
    public class UserAuthModel
    {
        public UserLoginModel UserLoginModel { get; set; }
        public UserRegisterModel UserRegisterModel { get; set; }
    }

    public class UserLoginModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class UserRegisterModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirm field is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match ")]
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class LibrarianPasswordModel
    {
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Password confirm field is required")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match ")]
        [Display(Name = "Confirm new password")]
        [DataType(DataType.Password)]
        public string NewPasswordConfirm { get; set; }
    }
}