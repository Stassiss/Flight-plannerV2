using System.Collections.Generic;
using Entities.DataTransferObjects.Airports;

namespace Contracts
{
    public interface IAirportRepository
    {
        List<AirportOutDto> SearchAirports(string search);
    }
}
