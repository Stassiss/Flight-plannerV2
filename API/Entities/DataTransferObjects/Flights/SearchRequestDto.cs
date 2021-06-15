﻿using System.ComponentModel.DataAnnotations;

namespace API.Entities.DataTransferObjects.Flights
{
    public class SearchRequestDto
    {
        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public string DepartureDate { get; set; }
    }
}
