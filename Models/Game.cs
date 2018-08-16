using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace gamenight.Models
{
    public class Game : BaseEntity
    {
        public int GameId { get; set; }
        public string Title { get; set; }
        public string Platform { get; set; }
        public DateTime Date{ get; set; }
        public DateTime Time { get; set; }
        public string Duration { get; set; }
        public string TimeSpan { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("CreatedBy")]
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public List<Joiner> Joiners { get; set; }

        public Game ()
        {
            Joiners = new List<Joiner>();
        }
    }
}