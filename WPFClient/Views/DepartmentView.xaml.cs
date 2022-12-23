using System.Windows;

namespace WPFClient
{
    public partial class DepartmentView : Window
    {
        private ApplicationVM _vm;

        public DepartmentView(ApplicationVM vm)
        {
            InitializeComponent();
            DataContext = _vm = vm;
        }
    }
}
