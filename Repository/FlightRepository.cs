﻿using System;
using System.Linq;
using Contracts;
using Entities;
using Entities.DataTransferObjects.Flights;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Exceptions;
using Repository.Helpers;
using Repository.Helpers.Converter;

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
                CheckDateFormat(flightInDto);

                CompareAirportsNames(flightInDto.From.AirportName,
                    flightInDto.To.AirportName);

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
                throw new NotFoundException(
                    nameof(FlightRepository),
                    nameof(GetFlightById),
                    $"{id}");
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
                throw new NotFoundException(nameof(FlightRepository), nameof(Delete), $"{id}");
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

                try
                {
                    CompareAirportsNames(x.From.AirportName, flightInDto.From.AirportName);
                    return;
                }
                catch (SameAirportException e)
                {
                    Console.WriteLine(e);
                    throw new SameFlightException(nameof(FlightRepository), nameof(CheckIfFlightInDb));
                }
            });
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
