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

        //Выбранный работник из таблицы
        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set 
            {
                selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
                TempEmployee = (Employee)(value?.Clone()); //Выглядит как костыль, но ничего лучше я не придумал
            }
        }

        //Временная копия для редактирования выбранного работника
        private Employee tempEmployee;
        public Employee TempEmployee
        {
            get => tempEmployee;
            set
            {
                tempEmployee = value;
                OnPropertyChanged(nameof(TempEmployee));
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
        public ICommand WindowClosedCommand => windowClosedCommand ??= new Command(() => db.Dispose());

        private ICommand searchCommand;
        public ICommand SearchCommand => searchCommand ??= new Command(() => LoadEmployees());

        //Сброс поиска
        private ICommand clearSearchCommand;
        public ICommand ClearSearchCommand => clearSearchCommand ??= new Command(() => 
            {
                SearchText = "";
                LoadEmployees();
            }
        );

        private ICommand fillDbCommand;
        public ICommand FillDbCommand => fillDbCommand ??= new Command(() => FillDbWithRandomEmployees());

        private ICommand addEmployeeCommand;
        public ICommand AddEmployeeCommand => addEmployeeCommand ??= new Command(() => AddEmployee());

        private ICommand saveEmployeeCommand;
        public ICommand SaveEmployeeCommand => saveEmployeeCommand ??= new Command(() => SaveSelectedEmployee());

        private ICommand removeEmployeeCommand;
        public ICommand RemoveEmployeeCommand => removeEmployeeCommand ??= new Command(() => RemoveEmployee());

        // Открытие окна для добавления отделов
        private ICommand openDepartmentViewCommand;
        public ICommand OpenDepartmentViewCommand => openDepartmentViewCommand ??= new Command(() => 
            new DepartmentView(new DepartmentVM(Departments)).Show()
        );

        #endregion

        #region methods

        //Загрузка данных при запуске приложения
        private void LoadInitialData()
        {
            try
            {
                if (!db.Database.CanConnect())
                {
                    db.Database.EnsureCreated();
                }
                RefillCollections();
            }
            catch
            {
                DialogTextColor = Color.Red;
                DialogText = DialogPhrase.DbConnectError;
            }
        }

        //Перезаполнение коллекций для обновления данных на вью
        private void RefillCollections()
        {
            LoadDeaprtments();
            LoadEmployees();
        }

        private void LoadDeaprtments()
        {
            db.Departments.Load();
            var departmentsList = db.Departments
                .Select(d => new Department(d.Id, d.Name))
                .ToList()
                .OrderBy(d => d.Name);
            Departments = new ObservableCollection<Department>(departmentsList);
        }

        //Подгрузка сотрудников с фильтрацией
        private void LoadEmployees()
        {
            db.Employees.Load();
            int id = SelectedEmployee?.Id ?? default;
            string text = searchText.ToLower();
            var employeesList = db.Employees
                .Select(e => new Employee(e))
                .ToList()
                .Where(e =>
                    e.LastName.ToLower().Contains(text) ||
                    e.FirstName.ToLower().Contains(text) ||
                    e.FatherName.ToLower().Contains(text))
                .OrderBy(e => e.LastName);
            Employees = new ObservableCollection<Employee>(employeesList);
            if (id != default)
            {
                SelectedEmployee = Employees.FirstOrDefault(e => e.Id == id);
            }
        }

        //Подсказка "как добавить сотрудника"
        private void AddEmployee()
        {
            if (SelectedEmployee != null && SelectedEmployee.Id != default)
            {
                SelectedEmployee = null;
            }
            DialogTextColor = Color.Black;
            DialogText = DialogPhrase.FillAndSave;
            if (SelectedEmployee == null || !HasEmployeeEmptyProperties(SelectedEmployee))
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
            dbEmployee.DepartmentId = Departments.First(d => d.Name == employee.DepartmentName).Id;
        }

        //Проверка на наличие незаполненных строковых свойств
        private bool HasEmployeeEmptyProperties(Employee employee)
        {
            return employee.GetType().GetProperties()
                .Where(p => p.PropertyType == typeof(string))
                .Select(p => p.GetValue(employee) as string)
                .Any(value => string.IsNullOrEmpty(value));
        }
        
        //Сохранение текущего сотрудника в базу
        private void SaveSelectedEmployee()
        {
            Database.Employee currentEmployee;

            TempEmployee ??= new Employee();
            if (HasEmployeeEmptyProperties(TempEmployee))
            {
                DialogTextColor = Color.Red;
                DialogText = DialogPhrase.FillTheFields;
                return;
            }

            try
            {
                SelectedEmployee = TempEmployee;
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
                    MapEmployee(SelectedEmployee, currentEmployee);
                    db.Add(currentEmployee);
                }
                db.SaveChanges();
                LoadEmployees();
                DialogTextColor = Color.Black;
                DialogText = DialogPhrase.Saved;
            }
            catch
            {
                DialogTextColor = Color.Red;
                DialogText = DialogPhrase.SaveToDbError;
            }
        }

        //Удаление сотрудника из базы
        private void RemoveEmployee()
        {
            if (SelectedEmployee.Id != default)
            {
                try
                {
                    int employeeId = SelectedEmployee.Id;
                    var currentEmployee = db.Employees.First(e => e.Id == employeeId);
                    if (currentEmployee != null)
                    {
                        Employees.Remove(Employees.First(e => e.Id == employeeId));
                        db.Remove(currentEmployee);
                        db.SaveChanges();
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

        //Заполнение базы сгенерированными сотрудниками (для упрощения тестирования)
        private void FillDbWithRandomEmployees() 
        {
            var rdg = new RandomDataGenerator(db);
            rdg.GenerateRandomEmployees();
            RefillCollections();
            DialogTextColor = Color.Black;
            DialogText = DialogPhrase.RandomEmployeesGenerated;
        }

        #endregion
    }
}
