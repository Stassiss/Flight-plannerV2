﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace API.Entities.Models
{
    public class Flight
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public Airport From { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public Airport To { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string Carrier { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public DateTime DepartureTime { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public DateTime ArrivalTime { get; set; }
    }
}
