using System;
using System.ComponentModel.DataAnnotations;
using Converter;
using Entities.Exceptions;

namespace Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    class ValidateDateAttribute : ValidationAttribute
    {
        private readonly string _departureTime;

        public ValidateDateAttribute(string departureTime)
        {
            _departureTime = departureTime;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var arrivalTime = (string)value;

            if (string.IsNullOrEmpty(arrivalTime))
            {
                return null;
            }

            var property = validationContext.ObjectType.GetProperty(_departureTime);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var departureTime = (string)property.GetValue(validationContext.ObjectInstance);

            if (string.IsNullOrEmpty(departureTime))
            {
                return null;
            }

            CheckDateFormat(departureTime, arrivalTime);

            return ValidationResult.Success;
        }

        private void CheckDateFormat(string departureTime, string arrivalTime)
        {
            var dateTimeArrival = arrivalTime.ConvertStringToDateTime();
            var dateTimeDeparture = departureTime.ConvertStringToDateTime();

            var dateArrivalTimeString = dateTimeArrival.ConvertDateTimeToString();
            var dateDepartureTimeString = dateTimeDeparture.ConvertDateTimeToString();

            if (!arrivalTime.Equals(dateArrivalTimeString))
            {
                throw new DateFormatException("Arrival time is not in correct format!");
            }

            if (!departureTime.Equals(dateDepartureTimeString))
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
