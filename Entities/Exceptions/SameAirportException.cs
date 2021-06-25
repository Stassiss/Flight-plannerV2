using System;

namespace Entities.Exceptions
{
    public class SameAirportException : Exception
    {
        public SameAirportException() : base("Airports cannot be the same!")
        {
        }
    }
}
