using Contracts;
using Entities.DataTransferObjects.Flights;
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
            var flightOutDto = _repository.GetFlightById(id);

            return Ok(flightOutDto);
        }

        [HttpPut("flights")]
        public IActionResult PutFlight([FromBody] FlightInDto flightInDto)
        {
            var flightOutDto = _repository.PutFlight(flightInDto);

            return Created("Flight", flightOutDto);
        }

        [HttpDelete("flights/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);

            return Ok();
        }
    }
}
