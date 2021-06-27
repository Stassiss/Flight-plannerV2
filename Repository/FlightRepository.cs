using System.Linq;
using Contracts;
using Converter;
using Entities;
using Entities.DataTransferObjects.Flights;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Exceptions;
using Repository.Mapper;

namespace Repository
{
    public class FlightRepository : RepositoryBase<Flight>, IFlightRepository
    {
        private static readonly object _lock = new object();

        public FlightRepository(IAppDbContext dbContext) : base(dbContext)
        {
        }

        public FlightOutDto PutFlight(FlightInDto flightInDto)
        {
            lock (_lock)
            {
                flightInDto.CheckDateFormat();

                flightInDto.CheckIfAirportsAreTheSame();

                CheckIfFlightInDb(flightInDto);

                var flight = Map.MapFlightInDtoToFlight(flightInDto);

                Update(flight);
                Save();

                var flightOutDto = Map.MapFlightToFlightOutDto(flight);

                return flightOutDto;
            }
        }

        public FlightOutDto GetFlightById(int id)
        {
            var flight = GetFlightFromDbById(id);

            if (flight == null)
            {
                throw new NotFoundException($"{id}");
            }

            var flightOutDto = Map.MapFlightToFlightOutDto(flight);

            return flightOutDto;
        }

        public void Delete(int id)
        {
            var flight = GetFlightFromDbById(id);

            if (flight == null) return;

            _dbContext.Airports.Remove(flight.To);
            _dbContext.Airports.Remove(flight.From);

            Delete(flight);
            Save();
        }

        public void Clear()
        {
            _dbContext.Airports.RemoveRange(_dbContext.Airports);
            _dbContext.Flights.RemoveRange(_dbContext.Flights);
            Save();
        }

        public PageResult SearchFlights(FlightSearchRequestDto search)
        {
            search.CheckIfAirportsAreTheSame();

            var flightsFromDb = FindByCondition(x => x.DepartureTime.Date == search.DepartureDate.ConvertStringToDateTime().Date, true)
                .Include(f => f.From).Where(x => x.From.AirportName.Trim().ToLower() == search.From.TrimToLowerString())
                .Include(f => f.To).Where(x => x.To.AirportName.Trim().ToLower() == search.To.TrimToLowerString())
                .ToList();

            var flightsOutDto = flightsFromDb.Select(Map.MapFlightToFlightOutDto).ToList();

            return new PageResult(flightsOutDto);
        }

        private Flight GetFlightFromDbById(int id)
        {
            return FindByCondition(x => x.Id == id, true)
                .Include(f => f.From)
                .Include(f => f.To)
                .FirstOrDefault();
        }

        /// <summary>
        /// if flight exists throws SameFlightException
        /// </summary>
        /// <param name="flightInDto"></param>
        private void CheckIfFlightInDb(FlightInDto flightInDto)
        {
            var flights = FindByCondition(x => x.ArrivalTime == flightInDto.ArrivalTime.ConvertStringToDateTime() &&
                                                   x.DepartureTime ==
                                                   flightInDto.DepartureTime.ConvertStringToDateTime()
                                                   && x.Carrier.Trim().ToLower() ==
                                                   flightInDto.Carrier.TrimToLowerString(), true);
            if (!flights.Any()) return;

            var airportExists = flights
                .Include(f => f.From)
                .Any(f => f.From.AirportName.Trim().ToLower() == flightInDto.From.AirportName.TrimToLowerString());

            if (airportExists)
            {
                throw new SameFlightException();
            }
        }
    }
}
