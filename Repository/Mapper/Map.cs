using Converter;
using Entities.DataTransferObjects.Airports;
using Entities.DataTransferObjects.Flights;
using Entities.Models;

namespace Repository.Mapper
{
    public static class Map
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
            return new()
            {
                Id = flight.Id,
                From = MapAirportToAirportOutDto(flight.From),
                To = MapAirportToAirportOutDto(flight.To),
                Carrier = flight.Carrier,
                DepartureTime = flight.DepartureTime.ConvertDateTimeToString(),
                ArrivalTime = flight.ArrivalTime.ConvertDateTimeToString()
            };
        }
    }
}
