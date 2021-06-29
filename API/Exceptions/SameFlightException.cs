using System;

namespace API.Exceptions
{
    public class SameFlightException : Exception
    {
        public SameFlightException(string className, string methodName)
            : base($"ClassName: {className}. MethodName: {methodName}. Cannot add same flight twice!")
        {
        }
    }
}
