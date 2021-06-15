using System.ComponentModel.DataAnnotations;
using API.Entities.DataTransferObjects.Airports;

namespace API.Entities.Models
{
    public class Airport
    {
        public Airport(AirportInDto airportInDto)
        {
            Country = airportInDto.Country;
            City = airportInDto.City;
            AirportName = airportInDto.AirportName;
        }

        [Required(ErrorMessage = "Required field!")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string City { get; set; }

        [Required(ErrorMessage = "Required field!")]

        public string AirportName { get; set; }
    }
}
