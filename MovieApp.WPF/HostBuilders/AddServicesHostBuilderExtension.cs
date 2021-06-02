using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.AuthenticationServices;
using MovieApp.Domain.Services.EmailServices;
using MovieApp.Domain.Services.ReviewServices;
using MovieApp.EntityFramework;
using MovieApp.EntityFramework.Services;

namespace MovieApp.WPF.HostBuilders
{
    public static class AddServicesHostBuilderExtension
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                services.AddSingleton<IPasswordHasher, PasswordHasher>();

                services.AddSingleton<IUserDataService, UserDataService>();
                services.AddSingleton<IFilmReviewDataService, FilmReviewDataService>();
                services.AddSingleton<IFilmDataService, FilmDataService>();
                services.AddSingleton<IFilmCastDataService, FilmCastDataService>();
                services.AddSingleton<IActorDataService, ActorDataService>();

                services.AddTransient<IUnitOfWork, UnitOfWork>();

                string email = context.Configuration.GetValue<string>("email");
                string email_password = context.Configuration.GetValue<string>("email_password");
                services.AddSingleton<EmailService>(new EmailService(email, email_password));

                services.AddScoped<IAuthenticationService, AuthenticationService>();      
                services.AddScoped<IEmailService, EmailService>();
                services.AddScoped<ILeaveReviewService, LeaveReviewService>();
            });

            return host;
        }
    }
}
