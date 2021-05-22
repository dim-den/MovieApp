using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MovieApp.Domain.Models;

namespace MovieApp.Domain.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private const string _login = "movieappwpf@gmail.com";
        private const string _password = "MovieAppWPF123.";

        public async Task<string> SendMessage(User user, string body, string subject, int codeLength)
        {
            string code = GenerateRandomString(codeLength);

            string htmlBody = String.Format(body, user.Name, code);

            MailAddress from = new MailAddress(_login, "Movie app");
            MailAddress to = new MailAddress(user.Email);
            MailMessage message = new MailMessage(from, to)
            {
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25)
            {
                Credentials = new NetworkCredential(_login, _password),
                UseDefaultCredentials = false,
                EnableSsl = true,
            };

            await smtp.SendMailAsync(message);

            return code;
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new();

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
