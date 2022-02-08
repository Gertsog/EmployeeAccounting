using System.Windows;

namespace WPFClient
{
    public partial class DepartmentView : Window
    {
        private DepartmentVM vm;

        public DepartmentView(DepartmentVM vm)
        {
            InitializeComponent();
            DataContext = this.vm = vm;
        }
    }
}
