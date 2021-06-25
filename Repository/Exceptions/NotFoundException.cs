using System;

namespace Repository.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string id)
            : base($"Id {id} not found!")
        {
        }
        public NotFoundException()
            : base($"Nothing found!")
        {
        }
    }
}
