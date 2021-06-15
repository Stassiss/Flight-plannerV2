using API.Entities.DataTransferObjects.Flights;

namespace API.Contracts
{
    public interface IFlightRepository
    {
        FlightOutDto PutFlight(FlightInDto flightInDto);
        FlightOutDto GetFlightById(int id);
        void Delete(int id);
        void Clear();
    }
}
