using System;
using System.Collections.Generic;
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
        [HttpGet("flights/{id}")]
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

        [HttpPut("{id}")]
        public IActionResult Put(FlightInDto flightInDto)
        {
            return Ok();
        }

        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
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
