using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
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

        private string searchText;
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
            DialogText = "";
            DialogTextColor = Color.Black;
            SearchText = "";
            SelectedEmployee = new Employee();
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

        private ICommand addEmployee;
        public ICommand AddEmployee => addEmployee ??= new Command(() => AddNewEmployee());

        private ICommand removeEmployeeCommand;
        public ICommand RemoveEmployeeCommand => removeEmployeeCommand ??= new Command(() => RemoveEmployee());

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

        private void AddNewEmployee()
        {
            DialogTextColor = Color.Black;
            DialogText = DialogPhrase.FillAndSave;
            if (!HasEmployeeEmptyProperties(SelectedEmployee))
            {
                SelectedEmployee = new Employee();
            }
        }

        private void MapEmployee(Employee employee, Database.Employee dbEmployee)
        {
            dbEmployee.LastName = employee.LastName;
            dbEmployee.FirstName = employee.FirstName;
            dbEmployee.FatherName = employee.FatherName;
            dbEmployee.Position = employee.Position;
            dbEmployee.Salary = employee.Salary;
            dbEmployee.DepartmentId = Departments.First(d => d.Name == SelectedEmployee.DepartmentName).Id;
        }

        private bool HasEmployeeEmptyProperties(Employee employee)
        {
            return employee.GetType().GetProperties()
                .Where(pi => pi.PropertyType == typeof(string))
                .Select(pi => pi.GetValue(SelectedEmployee) as string)
                .Any(value => string.IsNullOrEmpty(value));
        }
                
        private void SaveSelectedEmployee()
        {
            Database.Employee currentEmployee;

            if (HasEmployeeEmptyProperties(SelectedEmployee))
            {
                DialogTextColor = Color.Red;
                DialogText = DialogPhrase.FillTheFields;
                return;
            }

            if (SelectedEmployee.Id != default)
            {
                currentEmployee = db.Employees.First(e => e.Id == SelectedEmployee.Id);
                currentEmployee.Id = SelectedEmployee.Id;
                MapEmployee(SelectedEmployee, currentEmployee);
                db.Update(currentEmployee);
            }
            else
            {
                currentEmployee = new Database.Employee();
                {
                    MapEmployee(SelectedEmployee, currentEmployee);
                    db.Add(currentEmployee);
                    DialogTextColor = Color.Black;
                    DialogText = DialogPhrase.Saved;
                }
            }
            db.SaveChanges();
            RefillCollections();
            SelectedEmployee = Employees.First(e => e.Id == currentEmployee.Id);
        }

        private void FillDBWithRandomEmployees() 
        {
            var rdg = new RandomDataGenerator(db);
            rdg.GenerateRandomEmployees();
            RefillCollections();
            DialogTextColor = Color.Black;
            DialogText = DialogPhrase.RandomEmployeesGenerated;
        }

        private void LoadInitialData()
        { 
            if (!db.Database.CanConnect())
            {
                db.Database.EnsureCreated();
            }
            RefillCollections();
        }

        private void RemoveEmployee()
        {
            if (SelectedEmployee.Id != default)
            {
                var currentEmployee = db.Employees.First(e => e.Id == SelectedEmployee.Id);
                if (currentEmployee != null)
                {
                    db.Remove(currentEmployee);
                    db.SaveChanges();
                    RefillCollections();
                    DialogTextColor = Color.Black;
                    DialogText = DialogPhrase.EmployeeDeleted;
                    return;
                }
            }
            DialogTextColor = Color.Red;
            DialogText = DialogPhrase.EmployeeDoesntExist;
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
