using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.State.Navigator;

namespace MovieApp.WPF.HostBuilders
{
    public static class AddStoresHostBuilderExtension
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddScoped<Account>();
                services.AddScoped<INavigator, Navigator>();
                services.AddScoped<IAuthenticator, Authenticator>();
            });

            return host;
        }
    }
}
