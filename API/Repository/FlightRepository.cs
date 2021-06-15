using System;
using System.Linq;
using API.Contracts;
using API.Entities;
using API.Entities.DataTransferObjects.Flights;
using API.Exceptions;
using API.Helper;
using API.Helper.Converter;

namespace API.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IAirportRepository _airportRepository;
        private readonly AppDbContext _dbContext;
        private object _lock = new object();
        public FlightRepository(IAirportRepository airportRepository, AppDbContext dbContext)
        {
            _airportRepository = airportRepository;
            _dbContext = dbContext;
        }

        public FlightOutDto PutFlight(FlightInDto flightInDto)
        {
            lock (_lock)
            {
                CheckDateFormat(flightInDto);

                CompareAirportsNames(flightInDto.From.AirportName,
                    flightInDto.To.AirportName);

                var flight = Mapper.MapFlightInDtoToFlight(flightInDto, _dbContext);

                try
                {
                    CheckIfFlightInDb(flightInDto);
                    _dbContext.Flights.Add(flight);
                    _airportRepository.AddAirport(flight.From);
                    _airportRepository.AddAirport(flight.To);
                }
                catch (SameAirportException e)
                {
                    Console.WriteLine(e);
                }

                var flightOutDto = Mapper.MapFlightToFlightOutDto(flight);

                return flightOutDto;
            }
        }

        public FlightOutDto GetFlightById(int id)
        {
            lock (_lock)
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
        }

        public void Delete(int id)
        {
            lock (_lock)
            {
                var flight = _dbContext.Flights.Find(x => x.Id == id);

                if (flight == null)
                {
                    throw new NotFoundException(nameof(FlightRepository), nameof(Delete), $"{id}");
                }

                _dbContext.Flights.Remove(flight);
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _dbContext.Flights.Clear();
                _dbContext.Airports.Clear();
            }
        }

        public PageResult SearchFlights(SearchRequestDto search)
        {
            lock (_lock)
            {
                var flights = _dbContext.Flights.Where(
                    x => x.DepartureTime.ConvertDateTimeToString() == search.DepartureDate
                         && x.From.AirportName.TrimToLowerString() == search.From.TrimToLowerString()
                         && x.To.AirportName.TrimToLowerString() == search.To.TrimToLowerString()).ToList();
                var flightsOutDto = flights.Select(Mapper.MapFlightToFlightOutDto).ToList();

                var result = new PageResult(flightsOutDto);

                return result;
            }
        }

        private void CheckIfFlightInDb(FlightInDto flightInDto)
        {
            lock (_lock)
            {
                var exists = CompareFlights(flightInDto);

                if (exists)
                {
                    throw new SameFlightException(nameof(FlightRepository), nameof(CheckIfFlightInDb));
                }
            }
        }

        private bool CompareFlights(FlightInDto flight)
        {
            lock (_lock)
            {
                return _dbContext.Flights.Any(x =>
                {
                    if (x.ArrivalTime.ConvertDateTimeToString() != flight.ArrivalTime ||
                        x.DepartureTime.ConvertDateTimeToString() != flight.DepartureTime) return false;

                    if (!string.Equals(x.Carrier, flight.Carrier, StringComparison.CurrentCultureIgnoreCase)) return false;

                    try
                    {
                        CompareAirportsNames(x.From.AirportName, flight.From.AirportName);
                        return false;
                    }
                    catch (SameAirportException e)
                    {
                        Console.WriteLine(e);
                        return true;
                    }
                });
            }
        }

        public void CompareAirportsNames(string a, string b)
        {
            if (string.Equals(a.TrimToLowerString(), b.TrimToLowerString()))
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
