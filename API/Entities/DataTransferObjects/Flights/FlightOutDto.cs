using API.Entities.DataTransferObjects.Airports;
using API.Entities.Models;
using API.Helper;
using API.Helper.Converter;

namespace API.Entities.DataTransferObjects.Flights
{
    public class FlightOutDto : FlightDtoBase<AirportOutDto>
    {
        public FlightOutDto(Flight flight)
        {
            Id = flight.Id;
            From = Mapper.MapAirportToAirportOutDto(flight.From);
            To = Mapper.MapAirportToAirportOutDto(flight.To);
            Carrier = flight.Carrier;
            DepartureTime = flight.DepartureTime.ConvertDateTimeToString();
            ArrivalTime = flight.ArrivalTime.ConvertDateTimeToString();
        }
    }
}
