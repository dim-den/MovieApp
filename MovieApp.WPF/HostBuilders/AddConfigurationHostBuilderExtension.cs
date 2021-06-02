using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MovieApp.WPF.HostBuilders
{
    public static class AddConfigurationHostBuilderExtension
    {
        public static IHostBuilder AddConfiguration(this IHostBuilder host)
        {
            host.ConfigureAppConfiguration(c =>
            {
                c.AddJsonFile("appsettings.json");
                c.AddEnvironmentVariables();
            });

            return host;
        }
    }
}