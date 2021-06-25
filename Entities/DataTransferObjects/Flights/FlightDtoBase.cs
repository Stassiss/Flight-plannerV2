using System.ComponentModel.DataAnnotations;
using Entities.DataTransferObjects.Airports;
using Entities.Exceptions;

namespace Entities.DataTransferObjects.Flights
{
    public abstract class FlightDtoBase<T> where T : AirportDtoBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public T From { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public T To { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string Carrier { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string DepartureTime { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string ArrivalTime { get; set; }

        public void CheckIfAirportsAreTheSame()
        {
            if (From.AirportName.Trim().ToLower().Equals(To.AirportName.Trim().ToLower()))
            {
                throw new SameAirportException();
            }
        }
    }
}
