using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Converter;
using Entities.DataTransferObjects.Airports;
using Entities.Exceptions;

namespace Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateAirportsAreNotTheSameAttribute : ValidationAttribute
    {
        private readonly string _from;

        public ValidateAirportsAreNotTheSameAttribute(string from)
        {
            _from = from;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            if (value == null)
            {
                return null;
            }

            var currentAirport = ((AirportDtoBase)value);

            Validator.TryValidateObject(currentAirport, new ValidationContext(currentAirport),
                new List<ValidationResult>());

            var currentValue = currentAirport.AirportName;


            if (string.IsNullOrEmpty(currentValue))
            {
                return null;
            }

            var property = validationContext.ObjectType.GetProperty(_from);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonAirport = ((AirportDtoBase)property.GetValue(validationContext.ObjectInstance));

            if (comparisonAirport == null)
            {
                return null;
            }

            Validator.TryValidateObject(comparisonAirport, new ValidationContext(comparisonAirport),
                new List<ValidationResult>());

            var comparisonValue = comparisonAirport.AirportName;

            if (comparisonValue == null)
            {
                return null;
            }

            if (!currentValue.TrimToLowerString().Equals(comparisonValue.TrimToLowerString()))
                return ValidationResult.Success;

            throw new SameAirportException();
        }
    }
}
