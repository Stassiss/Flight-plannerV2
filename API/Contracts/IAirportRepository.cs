using System.Collections.Generic;
using API.Entities.DataTransferObjects.Airports;

namespace API.Contracts
{
    public interface IAirportRepository
    {
        List<AirportOutDto> SearchAirports(string search);
    }
}
