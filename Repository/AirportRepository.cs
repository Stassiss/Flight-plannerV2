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
            var airportsFromDb = FindByCondition(x => x.Country.Trim().ToLower().Contains(search.TrimToLowerString())
                                                      || x.City.Trim().ToLower().Contains(search.TrimToLowerString())
                                                      || x.AirportName.Trim().ToLower().Contains(search.TrimToLowerString()), true).ToList();

            return airportsFromDb.Select(Map.MapAirportToAirportOutDto).ToList();
        }

        public new void Clear() => base.Clear();

        public new void Delete(Airport airport)
        {
            base.Delete(airport);
            Save();
        }
    }
}
