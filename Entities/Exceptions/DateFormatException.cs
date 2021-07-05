using System;

namespace Entities.Exceptions
{
    public class DateFormatException : Exception
    {
        public DateFormatException(string msg) : base($"Date Format Exception! {msg}")
        { }
    }
}
