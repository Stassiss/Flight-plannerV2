using Microsoft.VisualBasic;

namespace API.Entities.DataTransferObjects.Flights
{
    public abstract class FlightDtoBase<T> where T : class
    {
        public T From { get; set; }
        public T To { get; set; }
        public string Carrier { get; set; }
        public DateAndTime DepartureTime { get; set; }
        public DateAndTime ArrivalTime { get; set; }
    }
}
