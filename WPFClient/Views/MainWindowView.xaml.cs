using System.Windows;

namespace WPFClient
{
    public partial class MainWindowView : Window
    {
        private ApplicationVM vm;

        public MainWindowView(ApplicationVM vm)
        {
            InitializeComponent();
            DataContext = this.vm = vm;
        }
    }
}
