using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.Domain.Services.EmailServices
{
    public interface IEmailService
    {
        Task<string> SendMessage(User user, string body, string subject, int codeLength);
    }
}
