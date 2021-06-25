using System.Linq;
using Contracts;
using Converter;
using Entities;
using Entities.DataTransferObjects.Flights;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Exceptions;
using Repository.Helpers;

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

                var flight = Mapper.MapFlightInDtoToFlight(flightInDto);

                Update(flight);
                _dbContext.SaveChanges();

                var flightOutDto = Mapper.MapFlightToFlightOutDto(flight);

                return flightOutDto;
            }
        }

        public FlightOutDto GetFlightById(int id)
        {
            var flight = FindByCondition(x => x.Id == id, true)
                .Include(f => f.From)
                .Include(f => f.To).FirstOrDefault();

            if (flight == null)
            {
                throw new NotFoundException($"{id}");
            }

            var flightOutDto = Mapper.MapFlightToFlightOutDto(flight);

            return flightOutDto;
        }

        public void Delete(int id)
        {
            var flight = FindByCondition(x => x.Id == id, true)
                .Include(f => f.From)
                .Include(f => f.To).FirstOrDefault();

            if (flight == null)
            {
                throw new NotFoundException($"{id}");
            }

            _dbContext.Airports.Remove(flight.To);
            _dbContext.Airports.Remove(flight.From);

            Delete(flight);
            _dbContext.SaveChanges();
        }

        public void Clear()
        {
            _dbContext.Airports.RemoveRange(_dbContext.Airports);
            _dbContext.Flights.RemoveRange(_dbContext.Flights);
            _dbContext.SaveChanges();
        }

        public PageResult SearchFlights(FlightSearchRequestDto search)
        {
            search.CheckIfAirportsAreTheSame();

            var flightsFromDb = FindAll(true)
                .Include(f => f.From)
                .Include(f => f.To)
                .ToList();

            var flights = flightsFromDb.Where(
                x => x.DepartureTime.ConvertDateTimeToString().Substring(0, 10) == search.DepartureDate
                     && x.From.AirportName.TrimToLowerString() == search.From.TrimToLowerString()
                     && x.To.AirportName.TrimToLowerString() == search.To.TrimToLowerString()).ToList();

            var flightsOutDto = flights.Select(Mapper.MapFlightToFlightOutDto).ToList();

            var result = new PageResult(flightsOutDto);

            return result;
        }

        private void CheckIfFlightInDb(FlightInDto flightInDto)
        {
            var flights = FindAll(true)
                .Include(f => f.From)
                .Include(f => f.To)
                .ToList();

            flights.ForEach(x =>
            {
                if (x.ArrivalTime.ConvertDateTimeToString() != flightInDto.ArrivalTime ||
                    x.DepartureTime.ConvertDateTimeToString() != flightInDto.DepartureTime) return;

                if (!string.Equals(x.Carrier.TrimToLowerString(), flightInDto.Carrier.TrimToLowerString())) return;

                if (x.From.AirportName.TrimToLowerString().Equals(flightInDto.From.AirportName.TrimToLowerString()))
                {
                    throw new SameFlightException();
                }
            });
        }
    }
}
