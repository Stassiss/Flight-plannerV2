using Entities;
using Entities.DataTransferObjects.Flights;

namespace Contracts
{
    public interface IFlightRepository
    {
        FlightOutDto PutFlight(FlightInDto flightInDto);
        FlightOutDto GetFlightById(int id);
        void Delete(int id);
        void Clear();
        PageResult SearchFlights(FlightSearchRequestDto search);
    }
}
