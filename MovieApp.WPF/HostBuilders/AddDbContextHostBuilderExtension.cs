using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieApp.EntityFramework;

namespace MovieApp.WPF.HostBuilders
{
    public static class AddDbContextHostBuilderExtension
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                bool remoteMode = context.Configuration.GetValue<bool>("remoteMode");
                string connectionString = remoteMode ? context.Configuration.GetConnectionString("remote") : context.Configuration.GetConnectionString("local");

                services.AddDbContext<MovieAppDbContext>(o => o.UseSqlServer(connectionString));
                services.AddSingleton<MovieAppDbContextFactory>(new MovieAppDbContextFactory(remoteMode, connectionString));
            });

            return host;
        }
    }
}
