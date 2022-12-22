using Grpc.Net.Client;
using ServiceConnector.Protos;

namespace ServiceConnector
{
    public class GrpcConnector : IServiceConnector
    {
        private GrpcChannel _channel;
        private Data.DataClient _client;

        public GrpcConnector()
        {
            _channel = GrpcChannel.ForAddress("https://localhost:7027");
            _client = new Data.DataClient(_channel);
        }

        ~GrpcConnector()
        {
            _channel.Dispose();
        }

        public async Task<int> AddDepartmentAsync(Common.Models.Department department)
        {
            var mapper = new Mapper();
            var request = mapper.MapDepartment(department);
            var response = await _client.AddDepartmentAsync(new DepartmentRequest { Department = request });

            return response.StatusCode;
        }

        public async Task<int> AddEmployeeAsync(Common.Models.Employee employee)
        {
            var mapper = new Mapper();
            var request = mapper.MapEmployee(employee);
            var response = await _client.AddEmployeeAsync(new EmployeeRequest { Employee = request });

            return response.StatusCode;
        }

        public async Task<int> CheckConnectionAsync()
        {
            var response = await _client.CheckConnectionAsync(new Empty());

            return response.StatusCode;
        }

        public async Task<int> CreateDBAsync()
        {
            var response = await _client.CreateDBAsync(new Empty());

            return response.StatusCode;
        }

        public async Task<int> GenerateRandomEmployeesAsync()
        {
            var response = await _client.GenerateRandomEmployeesAsync(new Empty());

            return response.StatusCode;
        }

        public async Task<List<Common.Models.Department>> GetDepartmentsAsync()
        {
            var response = await _client.LoadDepartmentsAsync(new Empty());
            var mapper = new Mapper();

            return response.Departments.Select(d => mapper.MapDepartment(d)).ToList();
        }

        public async Task<List<Common.Models.Employee>> GetEmployeesAsync()
        {
            var response = await _client.LoadEmployeesAsync(new Empty());
            var mapper = new Mapper();

            return response.Employees.Select(e => mapper.MapEmployee(e)).ToList();
        }

        public async Task<int> RemoveEmployeeAsync(Common.Models.Employee employee)
        {
            var mapper = new Mapper();
            var request = mapper.MapEmployee(employee);
            var response = await _client.RemoveEmployeeAsync(new EmployeeRequest { Employee = request });

            return response.StatusCode;
        }

        public async Task<int> UpdateEmployeeAsync(Common.Models.Employee employee)
        {
            var mapper = new Mapper();
            var request = mapper.MapEmployee(employee);
            var response = await _client.UpdateEmployeeAsync(new EmployeeRequest { Employee = request });

            return response.StatusCode;
        }

    }
}
