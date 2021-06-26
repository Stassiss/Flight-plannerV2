using System;
using Contracts;
using Entities.DataTransferObjects.Flights;
using Entities.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Repository.Exceptions;

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
        public IActionResult SearchAirports(string search)
        {
            var airports = _airportRepository.SearchAirports(search);

            return Ok(airports);
        }

        [HttpPost("flights/search")]
        public IActionResult SearchFlights([FromBody] FlightSearchRequestDto search)
        {
            var pageResult = _flightRepository.SearchFlights(search);

            return Ok(pageResult);
        }

        [HttpGet("flights/{id}")]
        public IActionResult GetFlightsById(int id)
        {
            var flightOutDto = _flightRepository.GetFlightById(id);

            return Ok(flightOutDto);
        }
    }
}
