using System.Collections.Generic;
using Entities.DataTransferObjects.Airports;
using Entities.Models;

namespace Contracts
{
    public interface IAirportRepository
    {
        List<AirportOutDto> SearchAirports(string search);

        public void Clear();

        public void Delete(Airport airport);
    }
}
