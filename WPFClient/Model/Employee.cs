using System.Windows.Controls;
using System.Windows.Input;

namespace WPFClient
{
    public class Employee : NotifyPropertyChanged
    {
        //Id сотрудника
        private int id;
        public int Id
        {
            get => id;
            set 
            { 
                id = value; 
                OnPropertyChanged(nameof(Id)); 
            }
        }

        //Фамилия
        private string lastName;
        public string LastName
        {
            get => lastName;
            set 
            { 
                lastName = value; 
                OnPropertyChanged(nameof(LastName)); 
            }
        }

        //Имя
        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        //Отчество
        private string fatherName;
        public string FatherName
        {
            get => fatherName;
            set 
            { 
                fatherName = value; 
                OnPropertyChanged(nameof(FatherName)); 
            }
        }

        //Должность
        private string position;
        public string Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        //Оклад
        private double salary;
        public double Salary
        {
            get => salary;
            set
            {
                if (value < 0)
                    salary = 0;
                else
                    salary = value;
                OnPropertyChanged(nameof(Salary));
            }
        }

        //Id отдела
        private int departmentId;
        public int DepartmentId
        {
            get => departmentId;
            set 
            { 
                departmentId = value; 
                OnPropertyChanged(nameof(DepartmentId)); 
            }
        }

        //Название отдела
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

        public Employee()
        {

        }

        //TODO: сделать класс с маппингом?
        public Employee(Database.Employee employee) 
        {
            Id = employee.Id;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            FatherName = employee.FatherName;
            Position = employee.Position;
            Salary = employee.Salary;
            DepartmentId = employee.DepartmentId;
            DepartmentName = employee.Department.Name;
        }

        // Маска для ввода числа в поле с окладом
        // Хотел оставить в VM, но не смог задать нужный контекст
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
    }
}