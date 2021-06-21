using System;
using Contracts;
using Entities.DataTransferObjects.Flights;
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
        private static readonly object _lock = new object();
        public CustomerApiController(IAirportRepository airportRepository, IFlightRepository flightRepository)
        {
            _airportRepository = airportRepository;
            _flightRepository = flightRepository;
        }
        [HttpGet("airports")]
        public IActionResult SearchAirports(string search)
        {
            lock (_lock)
            {
                try
                {
                    var airports = _airportRepository.SearchAirports(search);
                    return Ok(airports);
                }
                catch (NotFoundException e)
                {
                    Console.WriteLine(e);
                    return Ok();
                }
            }
        }

        [HttpPost("flights/search")]
        public IActionResult SearchFlights([FromBody] SearchRequestDto search)
        {
            lock (_lock)
            {
                try
                {
                    _flightRepository.CompareAirportsNames(search.From, search.To);
                    var pageResult = _flightRepository.SearchFlights(search);
                    return Ok(pageResult);
                }
                catch (SameAirportException e)
                {
                    Console.WriteLine(e);
                    return BadRequest();
                }
            }
        }

        [HttpGet("flights/{id}")]
        public IActionResult GetFlightsById(int id)
        {
            lock (_lock)
            {
                try
                {
                    var flightOutDto = _flightRepository.GetFlightById(id);
                    return Ok(flightOutDto);
                }
                catch (NotFoundException e)
                {
                    Console.WriteLine(e);
                    return NotFound();
                }
            }
        }
    }
}
