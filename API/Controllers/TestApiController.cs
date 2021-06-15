using API.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestApiController : ControllerBase
    {
        private readonly IFlightRepository _flightRepository;
        private static readonly object _lock = new object();
        public TestApiController(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        [HttpPost("clear")]
        public void Delete()
        {
            lock (_lock)
            {
                _flightRepository.Clear();
            }
        }
    }
}
