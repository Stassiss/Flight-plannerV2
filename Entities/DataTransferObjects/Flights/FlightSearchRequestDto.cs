using System.ComponentModel.DataAnnotations;
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

        public void CheckIfAirportsAreTheSame()
        {
            if (From.Trim().ToLower().Equals(To.Trim().ToLower()))
            {
                throw new SameAirportException();
            }
        }
    }
}
