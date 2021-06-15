using System;

namespace API.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string classname, string methodName, string id)
            : base($"ClassName: {classname}, MethodName: {methodName}. Id {id} not found!")
        {
        }
    }
}
