using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WPFClient.Database;

namespace WPFClient
{
    public class DepartmentVM : NotifyPropertyChanged
    {
        #region properties

        private EmployeeAccountingDbContext db;

        private ObservableCollection<Department> departments;
        public ObservableCollection<Department> Departments
        {
            get => departments;
            set
            {
                departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }

        private string departmentName;
        public string DepartmentName
        {
            get => departmentName;
            set 
            { 
                departmentName = value; 
                OnPropertyChanged(nameof(DepartmentName)); 
            }
        }

        private string dialogText;
        public string DialogText
        {
            get => dialogText;
            set
            {
                dialogText = value;
                OnPropertyChanged(nameof(DialogText));
            }
        }

        private string dialogTextColor;
        public string DialogTextColor
        {
            get => dialogTextColor;
            set
            {
                if (dialogTextColor != value)
                {
                    dialogTextColor = value;
                    OnPropertyChanged(nameof(DialogTextColor));
                }
            }
        }

        #endregion

        #region ctor

        public DepartmentVM()
        {
            db = new EmployeeAccountingDbContext();
            DialogText = "";
            DialogTextColor = Color.Black;
        }

        public DepartmentVM(ObservableCollection<Department> departments) : this()
        {
            Departments = departments;
        }

        #endregion

        #region commands

        private ICommand saveDepartment;
        public ICommand SaveDepartment => saveDepartment ??= new Command(() => TryAddDepartmentToDB());

        #endregion

        #region methods

        private void TryAddDepartmentToDB()
        {
            db.Departments.Load();
            if (db.Departments.Any(d => d.Name == DepartmentName))
            {
                DialogTextColor = Color.Red;
                DialogText = DialogPhrase.DepartmentAlreadyExists;
            }
            else
            {
                var department = new Database.Department() { Name = DepartmentName };
                db.Departments.Add(department);
                db.SaveChanges();
                department = db.Departments.First(d => d.Name == DepartmentName);
                Departments.Add(new Department(department.Id, department.Name));
                DialogTextColor = Color.Black;
                DialogText = DialogPhrase.Saved;
            }
        }

        #endregion
    }
}
