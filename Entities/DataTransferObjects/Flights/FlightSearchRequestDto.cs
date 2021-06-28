using System.ComponentModel.DataAnnotations;
using Entities.Attributes;

namespace Entities.DataTransferObjects.Flights
{
    public class FlightSearchRequestDto
    {
        [Required(ErrorMessage = "Required field!")]
        public string From { get; set; }

        [Required(ErrorMessage = "Required field!"), ValidateAirportsNamesAreNotTheSame("From")]
        public string To { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string DepartureDate { get; set; }
    }
}
