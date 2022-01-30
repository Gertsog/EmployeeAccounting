using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFClient.Database;

//Client - потому что изначально планировалось клиент-серверное приложение, но что-то пошло не так
namespace WPFClient
{
    public class MainWindowVM : NotifyPropertyChanged
    {
        #region properties

        private readonly EmployeeAccountingDbContext db;

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
            SelectedEmployee = new Employee();
            SearchText = "";
            DialogText = "";
            DialogTextColor = Color.Black;
        }

        #endregion

        #region commands

        private ICommand windowLoadedCommand;
        public ICommand WindowLoadedCommand => windowLoadedCommand ??= new Command(() => LoadInitialData());

        private ICommand windowClosedCommand;
        public ICommand WindowClosedCommand => windowClosedCommand ??= new Command(() => DisposeDb());

        private ICommand fillDbCommand;
        public ICommand FillDbCommand => fillDbCommand ??= new Command(() => FillDbWithRandomEmployees());

        private ICommand searchCommand;
        public ICommand SearchCommand => searchCommand ??= new Command(() => Search());

        private ICommand saveEmployeeCommand;
        public ICommand SaveEmployeeCommand => saveEmployeeCommand ??= new Command(() => SaveSelectedEmployee());

        private ICommand openDepartmentViewCommand;
        public ICommand OpenDepartmentViewCommand => openDepartmentViewCommand ??= new Command(() => OpenDepartmentView());

        private ICommand addEmployeeCommand;
        public ICommand AddEmployeeCommand => addEmployeeCommand ??= new Command(() => AddEmployee());

        private ICommand removeEmployeeCommand;
        public ICommand RemoveEmployeeCommand => removeEmployeeCommand ??= new Command(() => RemoveEmployee());

        #endregion

        #region methods

        //Перезаполнение коллекций для обновления данных на вью
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

        //Подсказка "как добавить сотрудника"
        private void AddEmployee()
        {
            DialogTextColor = Color.Black;
            DialogText = DialogPhrase.FillAndSave;
            if (!HasEmployeeEmptyProperties(SelectedEmployee))
            {
                SelectedEmployee = new Employee();
            }
        }

        //Маппинг класса в сущность EF
        private void MapEmployee(Employee employee, Database.Employee dbEmployee)
        {
            dbEmployee.LastName = employee.LastName;
            dbEmployee.FirstName = employee.FirstName;
            dbEmployee.FatherName = employee.FatherName;
            dbEmployee.Position = employee.Position;
            dbEmployee.Salary = employee.Salary;
            dbEmployee.DepartmentId = Departments.First(d => d.Name == SelectedEmployee.DepartmentName).Id;
        }

        //Проверка на наличие незаполненных строковых свойств
        private bool HasEmployeeEmptyProperties(Employee employee)
        {
            return employee.GetType().GetProperties()
                .Where(pi => pi.PropertyType == typeof(string))
                .Select(pi => pi.GetValue(SelectedEmployee) as string)
                .Any(value => string.IsNullOrEmpty(value));
        }
        
        //Сохранение текущего сотрудника в базу
        private void SaveSelectedEmployee()
        {
            Database.Employee currentEmployee;

            if (HasEmployeeEmptyProperties(SelectedEmployee))
            {
                DialogTextColor = Color.Red;
                DialogText = DialogPhrase.FillTheFields;
                return;
            }

            try
            {
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
            catch
            {
                DialogTextColor = Color.Red;
                DialogText = DialogPhrase.SaveToDbError;
            }
        }

        //Заполнение базы сгенерированными сотрудниками
        private async Task FillDbWithRandomEmployees() 
        {
            await new Task(() =>
            {
                var rdg = new RandomDataGenerator(db);
                rdg.GenerateRandomEmployees();
                RefillCollections();
                DialogTextColor = Color.Black;
                DialogText = DialogPhrase.RandomEmployeesGenerated;
            });            
        }

        //Загрузка данных при запуске приложения
        private void LoadInitialData()
        { 
            if (!db.Database.CanConnect())
            {
                db.Database.EnsureCreated();
            }
            RefillCollections();
        }

        //Удаление сотрудника из базы
        private void RemoveEmployee()
        {
            if (SelectedEmployee.Id != default)
            {
                try
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
                catch 
                {
                    DialogTextColor = Color.Red;
                    DialogText = DialogPhrase.DeleteFromDbError;
                    return;
                }

            }
            DialogTextColor = Color.Red;
            DialogText = DialogPhrase.EmployeeDoesntExist;
        }

        private void DisposeDb()
        {
            db.Dispose();
        }

        //Поиск среди сотрудников по ФИО
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

        //Открытие окна добавления отделов
        private void OpenDepartmentView()
        {
            var departmentVM = new DepartmentVM(Departments);
            var departmentView = new DepartmentView(departmentVM);
            departmentView.Show();
        }

        #endregion
    }
}
