using API.Entities.DataTransferObjects.Airports;
using API.Entities.DataTransferObjects.Flights;
using API.Entities.Models;

namespace API.Helper
{
    public static class Mapper
    {
        public static AirportOutDto MapAirportToAirportOutDto(Airport airport)
        {
            return new(airport);
        }

        public static Airport MapAirportInDtoToAirport(AirportInDto airportInDto)
        {
            return new(airportInDto);
        }

        public static Flight MapFlightInDtoToFlight(FlightInDto flightInDto)
        {
            var flight = new Flight(
                MapAirportInDtoToAirport(flightInDto.From),
                MapAirportInDtoToAirport(flightInDto.To),
                flightInDto.Carrier,
                flightInDto.DepartureTime,
                flightInDto.ArrivalTime);

            return flight;
        }
    }
}
