using API.Entities;
using API.Entities.DataTransferObjects.Airports;
using API.Entities.DataTransferObjects.Flights;
using API.Entities.Models;
using API.Helper.Converter;

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

        public static Flight MapFlightInDtoToFlight(FlightInDto flightInDto, AppDbContext dbContext)
        {
            var flight = new Flight(
                MapAirportInDtoToAirport(flightInDto.From),
                MapAirportInDtoToAirport(flightInDto.To),
                flightInDto.Carrier,
                flightInDto.DepartureTime.ConvertStringToDateTime(),
                flightInDto.ArrivalTime.ConvertStringToDateTime(), dbContext);

            return flight;
        }

        public static FlightOutDto MapFlightToFlightOutDto(Flight flight)
        {
            return new(flight);
        }
    }
}
