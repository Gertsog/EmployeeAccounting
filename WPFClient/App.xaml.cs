using System.Windows;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var viewModel = new MainWindowVM();
            var mainWindow = new MainWindowView(viewModel);
            mainWindow.Show();
        }
    }
}
