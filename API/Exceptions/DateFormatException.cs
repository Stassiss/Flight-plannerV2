using System;

namespace API.Exceptions
{
    public class DateFormatException : Exception
    {
        public DateFormatException(string className, string methodName)
            : base($"ClassName: {className}. MethodName: {methodName}. Date Format Exception!")
        { }
    }
}
