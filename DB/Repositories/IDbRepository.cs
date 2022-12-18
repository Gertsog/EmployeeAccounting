using DB.Data;

namespace DB.Repositories
{
    public interface IDbRepository
    {
        public int CheckConnection();
        public int CreateDB();
        public int GenerateRandomEmployees();

        public int AddDepartment(Department department);
        public List<Department> GetDepartments();

        public int AddEmployee(Employee employee);
        public List<Employee> GetEmployees();
        public int RemoveEmployee(Employee employee);
        public int UpdateEmployee(Employee employee);
    }
}
