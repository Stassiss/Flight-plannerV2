using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities.DataTransferObjects.Airports;
using Repository.Exceptions;
using Repository.Helpers;

namespace Repository
{
    public class AirportRepository : IAirportRepository
    {
        private readonly IAppDbContext _dbContext;
        private static readonly object _lock = new object();
        public AirportRepository(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<AirportOutDto> SearchAirports(string search)
        {
            lock (_lock)
            {
                var airportsFromDb = _dbContext.Airports.ToList();

                var airports = airportsFromDb.Where(x => TrimToLowerString(x.Country).Contains(TrimToLowerString(search))
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
