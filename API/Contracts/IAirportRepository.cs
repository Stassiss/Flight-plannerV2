using API.Entities.Models;

namespace API.Contracts
{
    public interface IAirportRepository
    {
        void AddAirport(Airport airport);
    }
}
