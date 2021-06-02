using System;

namespace MovieApp.Domain.Exceptions
{
    public class UsernameAlreadyExistsException : Exception
    {
        public string Username { get; set; }

        public UsernameAlreadyExistsException(string username)
        {
            Username = username;
        }

        public UsernameAlreadyExistsException(string message, string username) : base(message)
        {
            Username = username;
        }

        public UsernameAlreadyExistsException(string message, Exception innerException, string username) : base(message, innerException)
        {
            Username = username;
        }
    }
}