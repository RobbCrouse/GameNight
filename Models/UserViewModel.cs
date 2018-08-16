using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gamenight.Models
{
    public class UserViewModel : BaseEntity
    {
        
        [Required( ErrorMessage = "First Name is required.")]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        [Display(Prompt = "First Name ")]
        public string FirstName { get; set; }


        [Required( ErrorMessage = "Last Name is required.")]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        [Display(Prompt = "Last Name ")]
        public string LastName { get; set; }


        [Display(Prompt = "Gamertag ")]
        public string GamerTag { get; set; }


        [Display(Prompt = "Email ")]
        [Required( ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }


        [Required( ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$", ErrorMessage = "Password must contain at least 1 number, 1 letter, and 1 special character.")]
        [Display(Prompt = "Password ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        //[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$", ErrorMessage = "Password must contain at least 1 number, 1 letter, and 1 special character.")]
        [Display(Prompt = "Confirm Password ")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }


    }
}