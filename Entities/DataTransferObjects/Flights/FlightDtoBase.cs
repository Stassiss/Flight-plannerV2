using System.ComponentModel.DataAnnotations;
using Entities.Attributes;
using Entities.DataTransferObjects.Airports;

namespace Entities.DataTransferObjects.Flights
{
    public abstract class FlightDtoBase<T> where T : AirportDtoBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public T From { get; set; }

        [Required(ErrorMessage = "Required field!"), ValidateAirportsAreNotTheSame("From")]
        public T To { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string Carrier { get; set; }

        [Required(ErrorMessage = "Required field!")]
        public string DepartureTime { get; set; }

        [Required(ErrorMessage = "Required field!"), ValidateDate("DepartureTime")]
        public string ArrivalTime { get; set; }
    }
}
