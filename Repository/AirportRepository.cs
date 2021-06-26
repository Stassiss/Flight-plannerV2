using System.Collections.Generic;
using System.Linq;
using Contracts;
using Converter;
using Entities.DataTransferObjects.Airports;
using Entities.Models;
using Repository.Mapper;

namespace Repository
{
    public class AirportRepository : RepositoryBase<Airport>, IAirportRepository
    {
        public AirportRepository(IAppDbContext dbContext) : base(dbContext)
        {
        }

        public List<AirportOutDto> SearchAirports(string search)
        {
            var airportsFromDb = FindAll(true).ToList();

            var airports = airportsFromDb.Where(x => x.Country.TrimToLowerString().Contains(search.TrimToLowerString())
                                                          || x.City.TrimToLowerString().Contains(search.TrimToLowerString())
                                                          || x.AirportName.TrimToLowerString().Contains(search.TrimToLowerString())).ToList();

            return airports.Select(Map.MapAirportToAirportOutDto).ToList();
        }
    }
}
