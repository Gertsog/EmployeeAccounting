using System.Windows;

namespace WPFClient
{
    public partial class MainWindowView : Window
    {
        private MainWindowVM vm;

        public MainWindowView(MainWindowVM vm)
        {
            InitializeComponent();
            DataContext = this.vm = vm;
        }
    }
}
