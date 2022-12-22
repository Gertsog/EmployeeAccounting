using ServiceConnector;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

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
            services.AddSingleton<IServiceConnector>(new GrpcConnector());
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindowView>();
            mainWindow.Show();
        }
    }
}
