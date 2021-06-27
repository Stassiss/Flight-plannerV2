﻿using System.ComponentModel.DataAnnotations;
using Converter;
using Entities.Exceptions;

namespace Entities.DataTransferObjects.Flights
{
    public class FlightSearchRequestDto
    {
        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public string DepartureDate { get; set; }


        /// <summary>
        /// If Airports are the same throws SameAirportException
        /// </summary>
        public void CheckIfAirportsAreTheSame()
        {
            if (From.TrimToLowerString().Equals(To.TrimToLowerString()))
            {
                throw new SameAirportException();
            }
        }
    }
}
