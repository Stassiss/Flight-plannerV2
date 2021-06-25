using System.ComponentModel.DataAnnotations;
using Converter;
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

        public void CheckDateFormat()
        {
            var dateTimeArrival = ArrivalTime.ConvertStringToDateTime();
            var dateTimeDeparture = DepartureTime.ConvertStringToDateTime();

            var dateArrivalTimeString = dateTimeArrival.ConvertDateTimeToString();
            var dateDepartureTimeString = dateTimeDeparture.ConvertDateTimeToString();

            if (!ArrivalTime.Equals(dateArrivalTimeString))
            {
                throw new DateFormatException("Arrival time is not in correct format!");
            }

            if (!DepartureTime.Equals(dateDepartureTimeString))
            {
                throw new DateFormatException("Departure time is not in correct format!");
            }

            if (dateTimeDeparture >= dateTimeArrival)
            {
                throw new DateFormatException("Departure time cannot be grater or equal to arrival time!");
            }
        }
    }
}
