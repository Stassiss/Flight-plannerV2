using System.Collections.Generic;
using API.Entities.Models;

namespace API.Entities
{
    public static class AppDbContext
    {
        public static List<Flight> Flights => new List<Flight>();
        public static List<Airport> Airports => new List<Airport>();

    }
}
