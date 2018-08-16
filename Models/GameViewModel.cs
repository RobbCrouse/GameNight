using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gamenight.Models
{
    public class GameViewModel : BaseEntity
    {
        [Required( ErrorMessage = "A Game Title is required.  Let us know what you're playing.")]
        [MinLength(2)]
        [Display(Prompt = "Game Title")]
        public string Title { get; set; }


        [Required( ErrorMessage = "Which Platform are you playing this game on?" )]
        [MinLength(2)]
        [Display(Prompt = "Platform of Game; XB1, PC, boardgame, televised event, etc.")]
        public string Platform { get; set; }


        [Required]
        [Display(Prompt = "Date ")]
        [DataType( DataType.Date)]
        [FutureDate(ErrorMessage = "Date must be in the Future.")]
        public DateTime Date { get; set; }


        [Required]
        [Display(Prompt = "Time ")]
        [DataType( DataType.Time)]
        [FutureDate(ErrorMessage = "Time must be in the Future.")]
        public DateTime Time { get; set; }


        [Required( ErrorMessage = "How long are you planning to play?")]
        [Display(Prompt = "Duration of party ")]
        public string Duration { get; set; }


        public string TimeSpan { get; set; }


        [Required( ErrorMessage = "Please be a little more Descriptive, at least 5 characters.")]
        [MinLength(10)]
        [Display(Prompt = "Description  ")]
        public string Description { get; set; }
        

        public class FutureDateAttribute : ValidationAttribute
        {
            public FutureDateAttribute() {}

            public override bool IsValid(object value)
            {
                var submittedDate = ( DateTime )value;
                if( submittedDate > DateTime.Today )
                {
                    return true;
                }
                return false;
            }
        }
    }
}