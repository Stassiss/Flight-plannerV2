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
        public string Country { get; set; }
        public string City { get; set; }

        public string AirportName { get; set; }
    }
}
