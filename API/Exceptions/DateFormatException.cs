using System;

namespace API.Exceptions
{
    public class DateFormatException : Exception
    {
        public DateFormatException(string className, string methodName) : base($"ClassName: {className}. " +
                                                                               $"methodName: {methodName}." +
                                                                               $" Date Format Exception!")
        { }
    }
}
