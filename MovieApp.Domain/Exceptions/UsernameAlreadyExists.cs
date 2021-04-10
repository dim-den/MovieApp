using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Exceptions
{
    public class UsernameAlreadyExists : Exception
    {
        public string Username { get; set; }

        public UsernameAlreadyExists(string username)
        {
            Username = username;
        }

        public UsernameAlreadyExists(string message, string username) : base(message)
        {
            Username = username;
        }

        public UsernameAlreadyExists(string message, Exception innerException, string username) : base(message, innerException)
        {
            Username = username;
        }
    }
}
