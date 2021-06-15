using API.Contracts;
using API.Entities;
using API.Entities.Models;

namespace API.Repository
{
    public class AirportRepository : IAirportRepository
    {
        public void AddAirport(Airport airport)
        {
            AppDbContext.Airports.Add(airport);
        }
    }
}
