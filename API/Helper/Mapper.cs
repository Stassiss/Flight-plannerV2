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
            return new Airport()
            {
                Country = airportInDto.Country,
                City = airportInDto.City,
                AirportName = airportInDto.AirportName
            };
        }

        public static Flight MapFlightInDtoToFlight(FlightInDto flightInDto)
        {
            var flight = new Flight()
            {
                From = MapAirportInDtoToAirport(flightInDto.From),
                To = MapAirportInDtoToAirport(flightInDto.To),
                Carrier = flightInDto.Carrier,
                DepartureTime = flightInDto.DepartureTime.ConvertStringToDateTime(),
                ArrivalTime = flightInDto.ArrivalTime.ConvertStringToDateTime()
            };

            return flight;
        }

        public static FlightOutDto MapFlightToFlightOutDto(Flight flight)
        {
            return new(flight);
        }
    }
}
