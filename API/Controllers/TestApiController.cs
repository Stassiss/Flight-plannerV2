using Contracts;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestApiController : ControllerBase
    {
        private readonly IFlightRepository _flightRepository;
        public TestApiController(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        [HttpPost("clear")]
        public void Delete() => _flightRepository.Clear();

    }
}
