using System.Collections.Generic;
using System.Linq;
using API.Contracts;
using API.Entities;
using API.Entities.DataTransferObjects.Airports;
using API.Entities.Models;
using API.Exceptions;
using API.Helper;

namespace API.Repository
{
    public class AirportRepository : IAirportRepository
    {
        private readonly AppDbContext _dbContext;
        private object _lock = new object();
        public AirportRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddAirport(Airport airport)
        {
            lock (_lock)
            {
                _dbContext.Airports.Add(airport);
            }
        }

        public List<AirportOutDto> SearchAirports(string search)
        {
            lock (_lock)
            {
                var airports = _dbContext.Airports.Where(x => TrimToLowerString(x.Country).Contains(TrimToLowerString(search))
                                                              || TrimToLowerString(x.City).Contains(TrimToLowerString(search))
                                                              || TrimToLowerString(x.AirportName).Contains(TrimToLowerString(search))).ToList();
                if (!airports.Any())
                {
                    throw new NotFoundException(nameof(AirportInDto), nameof(SearchAirports), "no id");
                }

                return airports.Select(Mapper.MapAirportToAirportOutDto).ToList();
            }
        }

        private string TrimToLowerString(string str)
        {
            return str.ToLower().Trim();
        }
    }
}
