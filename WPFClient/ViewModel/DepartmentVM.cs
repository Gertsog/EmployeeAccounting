using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFClient.Database;

namespace WPFClient
{
    public class DepartmentVM : NotifyPropertyChanged
    {
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

        private string windowTitle;
        public string WindowTitle
        {
            get => windowTitle;
            set
            {
                if (windowTitle != value)
                {
                    windowTitle = value;
                    OnPropertyChanged(nameof(WindowTitle));
                }
            }
        }

        public DepartmentVM()
        {
            db = new EmployeeAccountingDbContext();
            WindowTitle = "Добавление отдела";
            DialogText = "";
            DialogTextColor = "Black";
        }

        public DepartmentVM(ObservableCollection<Department> departments) : this()
        {
            Departments = departments;
        }

        private ICommand saveDepartment;
        public ICommand SaveDepartment => saveDepartment ??= new Command(() => TryAddDepartmentToDB());

        private ICommand cancelAdding;

        public ICommand CancelAdding => cancelAdding ??= new Command(() => CloseWindow());


        private void TryAddDepartmentToDB()
        {
            db.Departments.Load();
            if (db.Departments.Any(d => d.Name == DepartmentName))
            {
                DialogTextColor = "Red";
                DialogText = "Такой отдел уже существует";
            }
            else
            {
                DialogTextColor = "Black";
                DialogText = "";
                var department = new Database.Department() { Name = DepartmentName };
                db.Departments.Add(department);
                db.SaveChanges();
                department = db.Departments.First(d => d.Name == DepartmentName);
                Departments.Add(new Department(department.Id, department.Name));
                DialogText = "Сохранено!";
            }
        }

        private void CloseWindow()
        {

        }
    }
}
