using DB.Data;

namespace DB.Repositories
{
    //В планах - добавить MongoDB и реализовать интерфейс под неё
    public interface IDbRepository
    {
        public int CheckDBConnection();
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
