using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Exceptions
{
    public class PasswordsMismatchException : Exception
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public PasswordsMismatchException(string password, string confirmPassword)
        {
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public PasswordsMismatchException(string message, string password, string confirmPassword) : base(message)
        {
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public PasswordsMismatchException(string message, Exception innerException, string password, string confirmPassword) : base(message, innerException)
        {
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}
