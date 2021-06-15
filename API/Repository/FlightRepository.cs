using API.Contracts;
using API.Entities;
using API.Entities.DataTransferObjects.Flights;
using API.Exceptions;
using API.Helper;

namespace API.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IAirportRepository _airportRepository;

        public FlightRepository(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public void PutFlight(FlightInDto flightInDto)
        {
            var flight = Mapper.MapFlightInDtoToFlight(flightInDto);

            _airportRepository.AddAirport(flight.From);
            _airportRepository.AddAirport(flight.To);

            AppDbContext.Flights.Add(flight);
        }

        public FlightOutDto GetFlightById(int id)
        {
            var flight = AppDbContext.Flights.Find(x => x.Id == id);
            if (flight == null)
            {
                throw new NotFoundException(
                    nameof(FlightRepository),
                    nameof(GetFlightById),
                    $"{id.ToString()}");
            }

            var flightOutDto = new FlightOutDto(flight);

            return flightOutDto;
        }

        public void Delete(int id)
        {
            var flight = AppDbContext.Flights.Find(x => x.Id == id);

            if (flight == null)
            {
                throw new NotFoundException(nameof(FlightRepository), nameof(Delete), $"{id.ToString()}");
            }

            AppDbContext.Flights.Remove(flight);
        }

        public void Clear()
        {
            AppDbContext.Flights.Clear();
            AppDbContext.Airports.Clear();
        }
    }
}
