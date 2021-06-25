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

                var flight = Mapper.MapFlightInDtoToFlight(flightInDto);


                try
                {
                    CheckIfFlightInDb(flightInDto);
                    _dbContext.Flights.Add(flight);
                    _dbContext.SaveChanges();
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
                var flight = _dbContext.Flights
                    .Include(f => f.From)
                    .Include(f => f.To).FirstOrDefault(x => x.Id == id);

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
        }

        public void Delete(int id)
        {
            lock (_lock)
            {
                var flight = _dbContext.Flights
                    .Include(f => f.From)
                    .Include(f => f.To).FirstOrDefault(x => x.Id == id);

                if (flight == null)
                {
                    throw new NotFoundException(nameof(FlightRepository), nameof(Delete), $"{id}");
                }

                _dbContext.Airports.RemoveRange(flight.To);
                _dbContext.Airports.RemoveRange(flight.From);
                _dbContext.Flights.Remove(flight);
                _dbContext.SaveChanges();
            }
        }

        public void Clear()
        {
            lock (_lock)
            {
                _dbContext.Flights.RemoveRange(_dbContext.Flights);
                _dbContext.Airports.RemoveRange(_dbContext.Airports);
                _dbContext.SaveChanges();
            }
        }

        public PageResult SearchFlights(SearchRequestDto search)
        {
            lock (_lock)
            {
                var flightsFromDb = _dbContext.Flights
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
                var flights = _dbContext.Flights
                    .Include(f => f.From)
                    .Include(f => f.To)
                    .ToList();

                return flights.Any(x =>
                {
                    if (x.ArrivalTime.ConvertDateTimeToString() != flight.ArrivalTime ||
                        x.DepartureTime.ConvertDateTimeToString() != flight.DepartureTime) return false;

                    if (!string.Equals(x.Carrier.TrimToLowerString(), flight.Carrier.TrimToLowerString())) return false;

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
