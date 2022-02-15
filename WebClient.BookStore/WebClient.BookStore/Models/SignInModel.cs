using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.BookStore.Models
{
    public class SignInModel
    {
        [Display(Name="Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Please provide your email address to login")]
        public string Email { get; set; }


        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please provide your password to login")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
