namespace WPFClient
{
    public class Employee : NotifyPropertyChanged
    {
        private int id;
        private string firstName;
        private string lastName;
        private string fatherName;
        private string position;
        private double salary;
        private int departmentId;
        private string departmentName;

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

        public int Id
        {
            get => id;
            set 
            { 
                id = value; 
                OnPropertyChanged("Id"); 
            }
        }
        public string FirstName
        {
            get => firstName;
            set 
            { 
                firstName = value; 
                OnPropertyChanged("FirstName"); 
            }
        }
        public string LastName
        {
            get => lastName;
            set 
            { 
                lastName = value; 
                OnPropertyChanged("LastName"); 
            }
        }
        public string FatherName
        {
            get => fatherName;
            set 
            { 
                fatherName = value; 
                OnPropertyChanged("FatherName"); 
            }
        }
        public string Position
        {
            get => position;
            set
            {
                position = value;
                /*if (value == "Уволен") // TODO: сделать константу или хранилище с должностями
                    salary = 0;*/
                OnPropertyChanged("Position");
            }
        }
        public double Salary
        {
            get => salary;
            set
            {
                if (value < 0) //TODO: вызвать ошибку
                    salary = 0;
                else
                    salary = value;
                OnPropertyChanged("Salary");
            }
        }
        public int DepartmentId
        {
            get => departmentId;
            set 
            { 
                departmentId = value; 
                OnPropertyChanged("DepartmentId"); 
            }
        }
        public string DepartmentName
        {
            get => departmentName;
            set 
            { 
                departmentName = value; 
                OnPropertyChanged("DepartmentName"); 
            }
        }

    }
}