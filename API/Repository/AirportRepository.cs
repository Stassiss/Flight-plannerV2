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

        public AirportRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddAirport(Airport airport)
        {
            _dbContext.Airports.Add(airport);
        }

        public List<AirportOutDto> SearchAirports(string search)
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

        private string TrimToLowerString(string str)
        {
            return str.ToLower().Trim();
        }
    }
}
