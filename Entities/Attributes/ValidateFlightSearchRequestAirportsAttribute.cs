using System;
using System.ComponentModel.DataAnnotations;
using Converter;
using Entities.Exceptions;

namespace Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidateFlightSearchRequestAirportsAttribute : ValidationAttribute
    {
        private readonly string _from;

        public ValidateFlightSearchRequestAirportsAttribute(string from)
        {
            _from = from;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;

            var currentValue = (string)value;

            if (string.IsNullOrEmpty(currentValue))
            {
                return null;
            }

            var property = validationContext.ObjectType.GetProperty(_from);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (string)property.GetValue(validationContext.ObjectInstance);

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

