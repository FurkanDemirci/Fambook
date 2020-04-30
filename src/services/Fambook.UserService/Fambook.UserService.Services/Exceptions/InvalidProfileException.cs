using System;

namespace Fambook.UserService.Services.Exceptions
{
    public class InvalidProfileException : Exception
    {
        public InvalidProfileException()
        {
        }

        public InvalidProfileException(string message)
            : base(message)
        {
        }

        public InvalidProfileException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
