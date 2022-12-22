using Grpc.Core;
using GrpcService.Protos;

namespace gRPCService.Services
{
    public class DataSender : Data.DataBase
    {
        private readonly DbService _dbService;

        public DataSender(DbService dbService)
        {
            _dbService = dbService;
        }

        /// <summary>
        /// Sends department list to client
        /// </summary>
        /// <param name="request">Empty request</param>
        /// <returns>Department list</returns>
        public override Task<DepartmentsResponse> LoadDepartments(Empty request, ServerCallContext context)
        {
            var deparmentsResponse = new DepartmentsResponse();
            var mapper = new GrpcService.Mapper();
            deparmentsResponse.Departments.AddRange(_dbService.GetDepartments()
                .Select(d => mapper.MapDepartment(d)));
            return Task.FromResult(deparmentsResponse);
        }

        /// <summary>
        /// Sends employee list to client
        /// </summary>
        /// <param name="request">Empty request</param>
        /// <returns>Employee list</returns>
        public override Task<EmployeesResponse> LoadEmployees(Empty request, ServerCallContext context)
        {
            var employeesResponse = new EmployeesResponse();
            var mapper = new GrpcService.Mapper();
            employeesResponse.Employees.AddRange(_dbService.GetEmployees()
                .Select(e => mapper.MapEmployee(e)));
            return Task.FromResult(employeesResponse);
        }

        /// <summary>
        /// Receive add department request from client
        /// </summary>
        /// <param name="request">Department</param>
        /// <returns>Status code</returns>
        public override Task<StatusResponse> AddDepartment(DepartmentRequest request, ServerCallContext context)
        {
            var mapper = new GrpcService.Mapper();
            var department = mapper.MapDepartment(request.Department);
            var result = _dbService.AddDepartment(department);

            return Task.FromResult(new StatusResponse { StatusCode = result });
        }

        /// <summary>
        /// Receive add employee request from client
        /// </summary>
        /// <param name="request">Employee</param>
        /// <returns>Status code</returns>
        public override Task<StatusResponse> AddEmployee(EmployeeRequest request, ServerCallContext context)
        {
            var mapper = new GrpcService.Mapper();
            var employee = mapper.MapEmployee(request.Employee);
            var result = _dbService.AddEmployee(employee);

            return Task.FromResult(new StatusResponse { StatusCode = result });
        }

        /// <summary>
        /// Receive remove employee request from client
        /// </summary>
        /// <param name="request">Employee</param>
        /// <returns>Status code</returns>
        public override Task<StatusResponse> RemoveEmployee(EmployeeRequest request, ServerCallContext context)
        {
            var mapper = new GrpcService.Mapper();
            var employee = mapper.MapEmployee(request.Employee);
            var result = _dbService.RemoveEmployee(employee);

            return Task.FromResult(new StatusResponse { StatusCode = result });
        }

        /// <summary>
        /// Receive update employee request from client
        /// </summary>
        /// <param name="request">Employee</param>
        /// <returns>Status code</returns>
        public override Task<StatusResponse> UpdateEmployee(EmployeeRequest request, ServerCallContext context)
        {
            var mapper = new GrpcService.Mapper();
            var employee = mapper.MapEmployee(request.Employee);
            var result = _dbService.UpdateEmployee(employee);

            return Task.FromResult(new StatusResponse { StatusCode = result });
        }

        /// <summary>
        /// Generate random employee data for database
        /// </summary>
        /// <param name="request">Empty request</param>
        /// <returns>Status code</returns>
        public override Task<StatusResponse> GenerateRandomEmployees(Empty request, ServerCallContext context)
        {
            var result = _dbService.GenerateRandomEmployees();

            return Task.FromResult(new StatusResponse { StatusCode = result });
        }

        /// <summary>
        /// Send database connection status to client
        /// </summary>
        /// <param name="request">Empty request</param>
        /// <returns>Status code</returns>
        public override Task<StatusResponse> CheckConnection(Empty request, ServerCallContext context)
        {
            var result = _dbService.CheckConnection();

            return Task.FromResult(new StatusResponse { StatusCode = result });
        }

        /// <summary>
        /// Recieve database creation request from client
        /// </summary>
        /// <param name="request">Empty request</param>
        /// <returns>Status code</returns>
        public override Task<StatusResponse> CreateDB(Empty request, ServerCallContext context)
        {
            var result = _dbService.CreateDB();

            return Task.FromResult(new StatusResponse { StatusCode = result });
        }

    }
}
