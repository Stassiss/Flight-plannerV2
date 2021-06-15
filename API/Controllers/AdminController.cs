using System;
using API.Contracts;
using API.Entities.DataTransferObjects.Flights;
using API.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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
                return NotFound();
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
                return Conflict();
            }
            catch (SameAirportException e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
            catch (DateFormatException e)
            {
                Console.WriteLine(e);
                return BadRequest();
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
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return Ok();
            }
        }

    }
}
