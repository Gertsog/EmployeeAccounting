using System.Windows;

namespace WPFClient
{
    public partial class MainWindowView : Window
    {
        private ApplicationVM _vm;

        public MainWindowView(ApplicationVM vm)
        {
            InitializeComponent();
            DataContext = _vm = vm;
        }
    }
}
