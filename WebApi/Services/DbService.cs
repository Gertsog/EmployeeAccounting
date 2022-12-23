using DB.Repositories;

namespace WebApi.Services
{
    public class DbService
    {
        private IDbRepository _dbRepository;
        private Mapper _mapper;

        public DbService(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
            _mapper = new Mapper();
        }

        public List<Models.Department> GetDepartments()
        {
            var departments = _dbRepository.GetDepartments()
                .Select(d => _mapper.MapDepartment(d))
                .ToList();

            departments ??= new();
            
            return departments;
        }

        public List<Models.Employee> GetEmployees()
        {
            var employees = _dbRepository.GetEmployees()
                .Select(e => _mapper.MapEmployee(e))
                .ToList();

            employees ??= new();

            return employees;
        }

        public int AddDepartment(Models.Department department)
        {
            var dbDepartment = _mapper.MapDepartment(department);

            return _dbRepository.AddDepartment(dbDepartment);
        }

        public int AddEmployee(Models.Employee employee)
        {
            var dbEmployee = _mapper.MapEmployee(employee);

            return _dbRepository.AddEmployee(dbEmployee);
        }

        public int RemoveEmployee(Models.Employee employee)
        {
            var dbEmployee = _mapper.MapEmployee(employee);

            return _dbRepository.RemoveEmployee(dbEmployee);
        }

        public int UpdateEmployee(Models.Employee employee)
        {
            var dbEmployee = _mapper.MapEmployee(employee);

            return _dbRepository.UpdateEmployee(dbEmployee);
        }

        public int GenerateRandomEmployees()
        {
            return _dbRepository.GenerateRandomEmployees();
        }

        public int CheckDbConnection()
        {
            return _dbRepository.CheckDbConnection();
        }

    }
}
