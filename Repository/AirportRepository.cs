using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities.DataTransferObjects.Airports;
using Entities.Models;
using Repository.Exceptions;
using Repository.Helpers;
using Repository.Helpers.Converter;

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
            if (!airports.Any())
            {
                throw new NotFoundException(nameof(AirportInDto), nameof(SearchAirports), "no id");
            }

            return airports.Select(Mapper.MapAirportToAirportOutDto).ToList();
        }
    }
}
