using System.Windows;

namespace WPFClient
{
    public partial class DepartmentView : Window
    {
        private ApplicationVM vm;

        public DepartmentView(ApplicationVM vm)
        {
            InitializeComponent();
            DataContext = this.vm = vm;
        }
    }
}
