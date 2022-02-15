using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.BookStore.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage ="Please provide first name")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please provide last name")]
        public string LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name ="Email Id")]
        [Required(ErrorMessage ="Please provide email id")]
        [EmailAddress(ErrorMessage ="Please provide a valid email id")]
        public string Email { get; set; }   

        [Display(Name = "Password")]
        [Compare("ConfirmPassword",ErrorMessage ="Confirm password doesn't match")]
        [Required(ErrorMessage = "Please provide strong password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
