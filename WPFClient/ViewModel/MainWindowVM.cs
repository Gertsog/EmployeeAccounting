using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WPFClient;
using WPFClient.Database;

namespace WPFClient
{
    public class MainWindowVM : NotifyPropertyChanged
    {
        #region properties

        private EmployeeAccountingDbContext db;

        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees
        {
            get => employees;
            set { 
                employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

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

        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set 
            { 
                selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
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

        private string searchText = "";
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }

        #endregion

        #region ctor

        public MainWindowVM()
        {
            db = new EmployeeAccountingDbContext();
            Employees = new ObservableCollection<Employee>();
            Departments = new ObservableCollection<Department>();
            WindowTitle = "Система учета сотрудников";
        }

        #endregion

        #region commands

        private ICommand windowLoaded;
        public ICommand WindowLoaded => windowLoaded ??= new Command(() => LoadInitialData());

        private ICommand windowClosed;
        public ICommand WindowClosed => windowClosed ??= new Command(() => DisposeDB());

        private ICommand fillDb;
        public ICommand FillDb => fillDb ??= new Command(() => FillDBWithRandomEmployees());

        private ICommand searchButtonClick;
        public ICommand SearchButtonClick => searchButtonClick ??= new Command(() => Search());

        private ICommand saveEmployee;
        public ICommand SaveEmployee => saveEmployee ??= new Command(() => SaveSelectedEmployee());

        private ICommand openDepartmentView;
        public ICommand OpenDepartmentView => openDepartmentView ??= new Command(() => CallDepartmentView());


        #endregion

        #region methods
        public void IsAllowedInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (!(char.IsDigit(e.Text, 0)
                || ((e.Text == ".")
                && !textBox.Text.Contains(".")
                && textBox.Text.Length != 0)))
            {
                e.Handled = true;
            }
        }

        private void RefillCollections()
        {
            LoadDeaprtments();
            LoadEmployees();
        }

        private void LoadDeaprtments()
        {
            db.Departments.Load();
            var departmentsList = db.Departments.Select(d => new Department(d.Id, d.Name)).ToList();
            Departments = new ObservableCollection<Department>(departmentsList);
        }

        private void LoadEmployees()
        {
            db.Employees.Load();
            var employeesList = db.Employees.Select(e => new Employee(e)).ToList();
            Employees = new ObservableCollection<Employee>(employeesList);
        }

        private void SaveSelectedEmployee()
        {
            var changedEmployee = db.Employees.First(e => e.Id == SelectedEmployee.Id);
            changedEmployee.Id = SelectedEmployee.Id;
            changedEmployee.LastName = SelectedEmployee.LastName;
            changedEmployee.FirstName = SelectedEmployee.FirstName;
            changedEmployee.FatherName = SelectedEmployee.FatherName;
            changedEmployee.Position = SelectedEmployee.Position;
            changedEmployee.Salary = SelectedEmployee.Salary;
            changedEmployee.DepartmentId = Departments.First(d => d.Name == SelectedEmployee.DepartmentName).Id;
            db.Update(changedEmployee);
            db.SaveChanges();
        }

        private async Task FillDBWithRandomEmployees() 
        {
            await Task.Run(() =>
            {
                var rdg = new RandomDataGenerator();
                var randomEmployees = rdg.GenerateRandomEmployees();
                rdg.FillDbWithEmployees(db, randomEmployees);
                RefillCollections();
            });
        }

        private void LoadInitialData()
        { 
            if (!db.Database.CanConnect())
            {
                db.Database.EnsureCreated();
            }
            RefillCollections();
        }

        private void DisposeDB()
        {
            db.Dispose();
        }

        private void Search()
        {
            LoadEmployees();

            string text = searchText.ToLower();
            var filteredEmployees = 
                Employees.Where(e =>
                    e.LastName.ToLower().Contains(text) ||
                    e.FirstName.ToLower().Contains(text) ||
                    e.FatherName.ToLower().Contains(text))
                .ToList();
            Employees = new ObservableCollection<Employee>(filteredEmployees);
        }

        private void CallDepartmentView()
        {
            var departmentVM = new DepartmentVM(Departments);
            var departmentView = new DepartmentView(departmentVM);
            departmentView.Show();
        }

        #endregion
    }
}
