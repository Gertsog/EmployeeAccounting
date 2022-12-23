using DB.Repositories;
using GrpcService;
using GrpcService.Protos;

namespace gRPCService.Services
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

        public DepartmentsResponse GetDepartments()
        {
            var deparmentsResponse = new DepartmentsResponse();
            deparmentsResponse.Departments.AddRange(_dbRepository.GetDepartments()
                .Select(d => _mapper.MapDepartment(d)));

            return deparmentsResponse;
        }

        public EmployeesResponse GetEmployees()
        {
            var employeesResponse = new EmployeesResponse();
            employeesResponse.Employees.AddRange(_dbRepository.GetEmployees()
                .Select(e => _mapper.MapEmployee(e)));

            return employeesResponse;
        }

        public int AddDepartment(Department protoDepartment)
        {
            var department = _mapper.MapDepartment(protoDepartment);

            return _dbRepository.AddDepartment(department);
        }

        public int AddEmployee(Employee protoEmployee)
        {
            var employee = _mapper.MapEmployee(protoEmployee);

            return _dbRepository.AddEmployee(employee);
        }

        public int RemoveEmployee(Employee protoEmployee)
        {
            var employee = _mapper.MapEmployee(protoEmployee);

            return _dbRepository.RemoveEmployee(employee);
        }

        public int UpdateEmployee(Employee protoEmployee)
        {
            var employee = _mapper.MapEmployee(protoEmployee);

            return _dbRepository.UpdateEmployee(employee);
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
