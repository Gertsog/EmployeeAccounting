using Grpc.Net.Client;
using ServiceConnector.Protos;

namespace ServiceConnector
{
    public class GrpcConnector : IServiceConnector
    {
        private GrpcChannel _channel;
        private Data.DataClient _client;
        private Mapper _mapper;

        public GrpcConnector(string serviceUrl)
        {
            _channel = GrpcChannel.ForAddress(serviceUrl);
            _client = new Data.DataClient(_channel);
            _mapper = new Mapper();
        }

        ~GrpcConnector()
        {
            _channel.Dispose();
        }

        public async Task<int> AddDepartmentAsync(Common.Models.Department department)
        {
            var request = _mapper.MapDepartment(department);
            var response = await _client.AddDepartmentAsync(new DepartmentRequest { Department = request });

            return response.StatusCode;
        }

        public async Task<int> AddEmployeeAsync(Common.Models.Employee employee)
        {
            var request = _mapper.MapEmployee(employee);
            var response = await _client.AddEmployeeAsync(new EmployeeRequest { Employee = request });

            return response.StatusCode;
        }

        //Избавиться бы от этого
        public async Task<int> CheckDbConnectionAsync()
        {
            var response = await _client.CheckConnectionAsync(new Empty());

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

            return response.Departments.Select(d => _mapper.MapDepartment(d)).ToList();
        }

        public async Task<List<Common.Models.Employee>> GetEmployeesAsync()
        {
            var response = await _client.LoadEmployeesAsync(new Empty());

            return response.Employees.Select(e => _mapper.MapEmployee(e)).ToList();
        }

        public async Task<int> RemoveEmployeeAsync(Common.Models.Employee employee)
        {
            var request = _mapper.MapEmployee(employee);
            var response = await _client.RemoveEmployeeAsync(new EmployeeRequest { Employee = request });

            return response.StatusCode;
        }

        public async Task<int> UpdateEmployeeAsync(Common.Models.Employee employee)
        {
            var request = _mapper.MapEmployee(employee);
            var response = await _client.UpdateEmployeeAsync(new EmployeeRequest { Employee = request });

            return response.StatusCode;
        }

    }
}
