using ServiceConnector;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Configuration;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ApplicationVM>();
            services.AddSingleton<MainWindowView>();

            var serviceUrl = ConfigurationManager.AppSettings["WebApiConnectionUrl"].ToString();
            var connector = new WebApiConnector(serviceUrl);
            //var serviceUrl = ConfigurationManager.AppSettings["GrpcConnectionUrl"].ToString();
            //var connector = new GrpcConnector(serviceUrl);
            services.AddSingleton<IServiceConnector>(connector);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindowView>();
            mainWindow.Show();
        }
    }
}
