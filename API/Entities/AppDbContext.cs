using System.Collections.Generic;
using API.Entities.Models;

namespace API.Entities
{
    public class AppDbContext
    {
        public AppDbContext()
        {
            Flights = new List<Flight>();
            Airports = new List<Airport>();
        }
        public List<Flight> Flights { get; }
        public List<Airport> Airports { get; }

    }
}
