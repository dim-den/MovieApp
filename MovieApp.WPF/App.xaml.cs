using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Domain.Services;
using MovieApp.Domain.Services.EmailServices;
using MovieApp.Domain.Services.AuthenticationServices;
using MovieApp.Domain.Services.ReviewServices;
using MovieApp.EntityFramework;
using MovieApp.WPF.ViewModels;
using MovieApp.EntityFramework.Services;
using Microsoft.AspNet.Identity;
using MovieApp.WPF.State.Navigator;
using MovieApp.WPF.State.Authentificator;
using MovieApp.WPF.ViewModels.Factories;
using MovieApp.WPF.Commands;
using MovieApp.Domain.Models;
using MovieApp.WPF.ViewModels.Builders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MovieApp.WPF.HostBuilders;

namespace MovieApp.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            SetAppCulture(new CultureInfo("en-US"));

            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                        .AddConfiguration()
                        .AddDbContext()
                        .AddServices()
                        .AddStores()
                        .AddViewModelBuilders()
                        .AddViewModels()
                        .AddViews();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private void SetAppCulture(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
