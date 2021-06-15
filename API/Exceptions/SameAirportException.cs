using System;

namespace API.Exceptions
{
    public class SameAirportException : Exception
    {
        public SameAirportException(string className, string methodName) : base($"ClassName: {className}. " +
            $"methodName: {methodName}." +
            $" Same Airport Error!")
        {
        }
    }
}
