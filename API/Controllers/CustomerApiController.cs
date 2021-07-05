using Contracts;
using Entities.DataTransferObjects.Flights;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IFlightRepository _flightRepository;
        public CustomerApiController(IAirportRepository airportRepository, IFlightRepository flightRepository)
        {
            _airportRepository = airportRepository;
            _flightRepository = flightRepository;
        }

        [HttpGet("airports")]
        public IActionResult SearchAirports(string search) => Ok(_airportRepository.SearchAirports(search));


        [HttpPost("flights/search")]
        public IActionResult SearchFlights([FromBody] FlightSearchRequestDto search) => Ok(_flightRepository.SearchFlights(search));


        [HttpGet("flights/{id}")]
        public IActionResult GetFlightsById(int id) => Ok(_flightRepository.GetFlightById(id));

    }
}
