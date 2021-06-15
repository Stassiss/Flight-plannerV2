using System;
using API.Contracts;
using API.Exceptions;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IAirportRepository _airportRepository;

        public CustomerApiController(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }
        [HttpGet("airports")]
        public IActionResult SearchAirports(string search)
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
}
