using System.ComponentModel.DataAnnotations;

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
    }
}
