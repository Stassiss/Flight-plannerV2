using System;
using Contracts;
using Entities.DataTransferObjects.Flights;
using Entities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Exceptions;

namespace API.Controllers
{
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IFlightRepository _repository;
        public AdminController(IFlightRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        [HttpGet("flights/{id}", Name = "Flight")]
        public IActionResult GetFlightsById(int id)
        {
            try
            {
                var flightOutDto = _repository.GetFlightById(id);
                return Ok(flightOutDto);
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return NotFound(e.Message);
            }
        }

        [HttpPut("flights")]
        public IActionResult PutFlight([FromBody] FlightInDto flightInDto)
        {
            try
            {
                var flightOutDto = _repository.PutFlight(flightInDto);

                return Created("Flight", flightOutDto);
            }
            catch (SameFlightException e)
            {
                Console.WriteLine(e);
                return Conflict(e.Message);
            }
            catch (SameAirportException e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
            catch (DateFormatException e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("flights/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok();
            }
        }
    }
}
