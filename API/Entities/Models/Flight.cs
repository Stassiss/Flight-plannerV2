using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace API.Entities.Models
{
    public class Flight
    {
        private readonly AppDbContext _dbContext;

        public Flight(Airport from,
            Airport to, string carrier,
            DateTime departureTime,
            DateTime arrivalTime,
            AppDbContext dbContext)
        {
            _dbContext = dbContext;
            Id = GenerateId();
            From = from;
            To = to;
            Carrier = carrier;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public Airport From { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public Airport To { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string Carrier { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public DateTime DepartureTime { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public DateTime ArrivalTime { get; set; }

        private int GenerateId()
        {
            return _dbContext.Flights.Any() ? _dbContext.Flights.Count + 1 : 1;
        }
    }
}
