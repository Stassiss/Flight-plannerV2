using System.ComponentModel.DataAnnotations;

namespace API.Entities.DataTransferObjects.Flights
{
    public abstract class FlightDtoBase<T>
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
    }
}
