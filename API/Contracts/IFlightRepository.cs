using API.Entities.DataTransferObjects.Flights;

namespace API.Contracts
{
    public interface IFlightRepository
    {
        void PutFlight(FlightInDto flightInDto);
        FlightOutDto GetFlightById(int id);
        void Delete(int id);
    }
}
