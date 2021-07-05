using System;

namespace Repository.Exceptions
{
    public class SameFlightException : Exception
    {
        public SameFlightException() : base(" Cannot add same flight twice!")
        {
        }
    }
}
