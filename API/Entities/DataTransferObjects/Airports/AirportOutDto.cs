using API.Entities.Models;

namespace API.Entities.DataTransferObjects.Airports
{
    public class AirportOutDto : AirportDtoBase
    {
        public AirportOutDto(Airport airport)
        {
            City = airport.City;
            AirportName = airport.AirportName;
            Country = airport.Country;
        }
    }
}
