using System.Linq;
using Microsoft.VisualBasic;

namespace API.Entities.Models
{
    public class Flight
    {
        public Flight(Airport from, Airport to, string carrier, DateAndTime departureTime, DateAndTime arrivalTime)
        {
            Id = GenerateId();
            From = from;
            To = to;
            Carrier = carrier;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }

        public int Id { get; set; }
        public Airport From { get; set; }
        public Airport To { get; set; }
        public string Carrier { get; set; }
        public DateAndTime DepartureTime { get; set; }
        public DateAndTime ArrivalTime { get; set; }

        private int GenerateId()
        {
            return AppDbContext.Flights.Any() ? AppDbContext.Flights.Count + 1 : 0;
        }
    }
}
