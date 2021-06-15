using API.Entities.DataTransferObjects.Airports;
using API.Entities.Models;
using API.Helper;

namespace API.Entities.DataTransferObjects.Flights
{
    public class FlightOutDto : FlightDtoBase<AirportOutDto>
    {
        public FlightOutDto(Flight flight)
        {
            From = Mapper.MapAirportToAirportOutDto(flight.From);
            To = Mapper.MapAirportToAirportOutDto(flight.To);
            Carrier = flight.Carrier;
            DepartureTime = flight.DepartureTime;
            ArrivalTime = flight.ArrivalTime;
        }
    }
}
