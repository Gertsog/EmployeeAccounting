using DB.Data;
using DB.Repositories;

namespace gRPCService.Services
{
    public class DbService
    {
        private IDbRepository _dbRepository;

        public DbService(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        public List<Department> GetDepartments()
        {
            return _dbRepository.GetDepartments();
        }

        public List<Employee> GetEmployees()
        {
            return _dbRepository.GetEmployees();
        }

        public int AddDepartment(Department department)
        {
            return _dbRepository.AddDepartment(department);
        }

        public int AddEmployee(Employee employee)
        {
            return _dbRepository.AddEmployee(employee);
        }

        public int RemoveEmployee(Employee employee)
        {
            return _dbRepository.RemoveEmployee(employee);
        }

        public int UpdateEmployee(Employee employee)
        {
            return _dbRepository.UpdateEmployee(employee);
        }

        public int GenerateRandomEmployees()
        {
            return _dbRepository.GenerateRandomEmployees();
        }
        public int CheckConnection()
        {
            return _dbRepository.CheckDBConnection();
        }

        public int CreateDB()
        {
            return _dbRepository.CreateDB();
        }

    }
}
