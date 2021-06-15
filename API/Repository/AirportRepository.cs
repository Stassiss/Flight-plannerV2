using API.Contracts;
using API.Entities;
using API.Entities.Models;

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
    }
}
