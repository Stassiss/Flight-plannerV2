using System.Collections.Generic;
using API.Entities.DataTransferObjects.Airports;
using API.Entities.Models;

namespace API.Contracts
{
    public interface IAirportRepository
    {
        void AddAirport(Airport airport);
        List<AirportOutDto> SearchAirports(string search);
    }
}
