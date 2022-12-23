using ServiceConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFClient
{
    public class ApplicationVM : NotifyPropertyChanged
    {
        #region properties

        private readonly IServiceConnector _serviceConnector;

        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees
        {
            get => employees;
            set
            {
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

        public ApplicationVM(IServiceConnector serviceConnector)
        {
            _serviceConnector = serviceConnector;
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
        public ICommand WindowLoadedCommand => windowLoadedCommand ??= new Command(async () => await LoadInitialDataAsync());

        private ICommand searchCommand;
        public ICommand SearchCommand => searchCommand ??= new Command(async () => await LoadEmployeesAsync());

        //Сброс поиска
        private ICommand clearSearchCommand;
        public ICommand ClearSearchCommand => clearSearchCommand ??= new Command(async () =>
            {
                SearchText = "";
                await LoadEmployeesAsync();
            }
        );

        private ICommand fillDbCommand;
        public ICommand FillDbCommand => fillDbCommand ??= new Command(async () => await FillDbWithRandomEmployeesAsync());

        private ICommand addEmployeeCommand;
        public ICommand AddEmployeeCommand => addEmployeeCommand ??= new Command(() => AddEmployee());

        private ICommand saveEmployeeCommand;
        public ICommand SaveEmployeeCommand => saveEmployeeCommand ??= new Command(async () => await SaveSelectedEmployeeAsync());

        private ICommand removeEmployeeCommand;
        public ICommand RemoveEmployeeCommand => removeEmployeeCommand ??= new Command(async () => await RemoveEmployeeAsync());

        // Открытие окна для добавления отделов
        private ICommand openDepartmentViewCommand;
        public ICommand OpenDepartmentViewCommand => openDepartmentViewCommand ??= new Command(() =>
            new DepartmentView(this).Show()
        );

        private ICommand saveDepartmentCommand;
        public ICommand SaveDepartmentCommand => saveDepartmentCommand ??= new Command(async () => await TryAddDepartmentToDbAsync());

        #endregion

        #region methods

        //Загрузка данных при запуске приложения
        private async Task LoadInitialDataAsync()
        {
            try
            {
                var result = await _serviceConnector.CheckDbConnectionAsync();
                if (result != 200)
                {
                    throw new Exception();
                }
                await RefillCollectionsAsync();
            }
            catch
            {
                DialogTextColor = Color.Red;
                DialogText = DialogPhrase.DbConnectError;
            }
        }

        //Перезаполнение коллекций для обновления данных на вью
        private async Task RefillCollectionsAsync()
        {
            await LoadDeaprtmentsAsync();
            await LoadEmployeesAsync();
        }

        private async Task LoadDeaprtmentsAsync()
        {
            var departments = await _serviceConnector.GetDepartmentsAsync();
            var departmentsList = departments
                .Select(d => new Department(d.Id, d.Name))
                .ToList()
                .OrderBy(d => d.Name);
            Departments = new ObservableCollection<Department>(departmentsList);
        }

        //Подгрузка сотрудников с фильтрацией
        private async Task LoadEmployeesAsync()
        {
            var employees = await _serviceConnector.GetEmployeesAsync();
            ulong id = SelectedEmployee?.Id ?? default;
            string text = searchText.ToLower();
            var employeesList = employees
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

        private void MapEmployee(Employee employee, Common.Models.Employee dbEmployee)
        {
            dbEmployee.LastName = employee.LastName;
            dbEmployee.FirstName = employee.FirstName;
            dbEmployee.FatherName = employee.FatherName;
            dbEmployee.Position = employee.Position;
            dbEmployee.Salary = employee.Salary;
            dbEmployee.DepartmentName = employee.DepartmentName;
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
        private async Task SaveSelectedEmployeeAsync()
        {
            Common.Models.Employee currentEmployee;

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
                    var employees = await _serviceConnector.GetEmployeesAsync();
                    currentEmployee = employees.First(e => e.Id == SelectedEmployee.Id);
                    currentEmployee.Id = SelectedEmployee.Id;
                    MapEmployee(SelectedEmployee, currentEmployee);
                    await _serviceConnector.UpdateEmployeeAsync(currentEmployee);
                }
                else
                {
                    currentEmployee = new Common.Models.Employee();
                    MapEmployee(SelectedEmployee, currentEmployee);
                    await _serviceConnector.AddEmployeeAsync(currentEmployee);
                }
                await LoadEmployeesAsync();
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
        private async Task RemoveEmployeeAsync()
        {
            if (SelectedEmployee.Id != default)
            {
                try
                {
                    ulong employeeId = SelectedEmployee.Id;
                    var employees = await _serviceConnector.GetEmployeesAsync();
                    var currentEmployee = employees.First(e => e.Id == employeeId);
                    if (currentEmployee != null)
                    {
                        Employees.Remove(Employees.First(e => e.Id == employeeId));
                        var result = await _serviceConnector.RemoveEmployeeAsync(currentEmployee);
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
        private async Task FillDbWithRandomEmployeesAsync()
        {
            await _serviceConnector.GenerateRandomEmployeesAsync();
            await RefillCollectionsAsync();
            DialogTextColor = Color.Black;
            DialogText = DialogPhrase.RandomEmployeesGenerated;
        }

        //Добавление нового отдела в базу
        private async Task TryAddDepartmentToDbAsync()
        {
            List<Common.Models.Department> departments;
            try
            {
                departments = await _serviceConnector.GetDepartmentsAsync();
            }
            catch
            {
                DialogTextColor = Color.Red;
                DialogText = DialogPhrase.LoadFromDbError;
                return;
            }

            if (departments.Any(d => d.Name.ToLower() == DepartmentName.ToLower()))
            {
                DialogTextColor = Color.Red;
                DialogText = DialogPhrase.DepartmentAlreadyExists;
            }
            else
            {
                try
                {
                    var department = new Common.Models.Department() { Name = DepartmentName };
                    await _serviceConnector.AddDepartmentAsync(department);
                    departments = await _serviceConnector.GetDepartmentsAsync();
                    department = departments.First(d => d.Name == DepartmentName);
                    Departments.Add(new Department(department.Id, department.Name));
                    DialogTextColor = Color.Black;
                    DialogText = DialogPhrase.Saved;
                }
                catch
                {
                    DialogTextColor = Color.Red;
                    DialogText = DialogPhrase.SaveToDbError;
                }
            }
        }
        #endregion
    }
}
