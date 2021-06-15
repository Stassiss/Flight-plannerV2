using System;
using System.Linq;
using API.Contracts;
using API.Entities;
using API.Entities.DataTransferObjects.Flights;
using API.Entities.Models;
using API.Exceptions;
using API.Helper;
using API.Helper.Converter;

namespace API.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IAirportRepository _airportRepository;
        private readonly AppDbContext _dbContext;

        public FlightRepository(IAirportRepository airportRepository, AppDbContext dbContext)
        {
            _airportRepository = airportRepository;
            _dbContext = dbContext;
        }

        public FlightOutDto PutFlight(FlightInDto flightInDto)
        {
            CheckDateFormat(flightInDto);

            CheckIfFlightInDb(flightInDto);

            CompareAirportsNames(Mapper.MapAirportInDtoToAirport(flightInDto.From),
                Mapper.MapAirportInDtoToAirport(flightInDto.To));

            var flight = Mapper.MapFlightInDtoToFlight(flightInDto, _dbContext);

            _airportRepository.AddAirport(flight.From);
            _airportRepository.AddAirport(flight.To);

            _dbContext.Flights.Add(flight);
            var flightOutDto = Mapper.MapFlightToFlightOutDto(flight);

            return flightOutDto;
        }

        public FlightOutDto GetFlightById(int id)
        {
            var flight = _dbContext.Flights.Find(x => x.Id == id);
            if (flight == null)
            {
                throw new NotFoundException(
                    nameof(FlightRepository),
                    nameof(GetFlightById),
                    $"{id}");
            }

            var flightOutDto = new FlightOutDto(flight);

            return flightOutDto;
        }

        public void Delete(int id)
        {
            var flight = _dbContext.Flights.Find(x => x.Id == id);

            if (flight == null)
            {
                throw new NotFoundException(nameof(FlightRepository), nameof(Delete), $"{id}");
            }

            _dbContext.Flights.Remove(flight);
        }

        public void Clear()
        {
            _dbContext.Flights.Clear();
            _dbContext.Airports.Clear();
        }

        private void CheckIfFlightInDb(FlightInDto flightInDto)
        {
            var exists = CompareFlights(flightInDto);

            if (exists)
            {
                throw new SameFlightException(nameof(FlightRepository), nameof(CheckIfFlightInDb));
            }
        }

        private bool CompareFlights(FlightInDto flight)
        {
            return _dbContext.Flights.Any(x =>
            {
                if (x.ArrivalTime.ConvertDateTimeToString() != flight.ArrivalTime ||
                    x.DepartureTime.ConvertDateTimeToString() != flight.DepartureTime) return false;

                if (!string.Equals(x.Carrier, flight.Carrier, StringComparison.CurrentCultureIgnoreCase)) return false;

                try
                {
                    CompareAirportsNames(x.From, Mapper.MapAirportInDtoToAirport(flight.From));
                    return false;
                }
                catch (SameAirportException e)
                {
                    Console.WriteLine(e);
                    return true;
                }
            });
        }

        private void CompareAirportsNames(Airport a, Airport b)
        {
            if (string.Equals(a.AirportName.Trim(), b.AirportName.Trim(),
                    StringComparison.CurrentCultureIgnoreCase))
            {
                throw new SameAirportException(nameof(FlightRepository), nameof(CompareAirportsNames));
            }
        }

        private void CheckDateFormat(FlightInDto flightInDto)
        {
            var dateTimeArrival = flightInDto.ArrivalTime.ConvertStringToDateTime();
            var dateTimeDeparture = flightInDto.DepartureTime.ConvertStringToDateTime();

            var dateArrivalTimeString = dateTimeArrival.ConvertDateTimeToString();
            var dateDepartureTimeString = dateTimeDeparture.ConvertDateTimeToString();

            if (!flightInDto.ArrivalTime.Equals(dateArrivalTimeString)
                || !flightInDto.DepartureTime.Equals(dateDepartureTimeString)
                || dateTimeDeparture >= dateTimeArrival)
            {
                throw new DateFormatException(nameof(FlightRepository), nameof(CheckDateFormat));
            }
        }
    }
}
