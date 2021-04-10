using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Exceptions
{
    public class EmailAlreadyExistsException : Exception
    {
        public string Email { get; set; }

        public EmailAlreadyExistsException(string email)
        {
            Email = email;
        }

        public EmailAlreadyExistsException(string message, string email) : base(message)
        {
            Email = email;
        }

        public EmailAlreadyExistsException(string message, Exception innerException, string email) : base(message, innerException)
        {
            Email = email;
        }
    }
}
