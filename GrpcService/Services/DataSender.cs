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
            var response = _dbService.GetDepartments();

            return Task.FromResult(response);
        }

        /// <summary>
        /// Sends employee list to client
        /// </summary>
        /// <param name="request">Empty request</param>
        /// <returns>Employee list</returns>
        public override Task<EmployeesResponse> LoadEmployees(Empty request, ServerCallContext context)
        {
            var response = _dbService.GetEmployees();

            return Task.FromResult(response);
        }

        /// <summary>
        /// Receive add department request from client
        /// </summary>
        /// <param name="request">Department</param>
        /// <returns>Status code</returns>
        public override Task<StatusResponse> AddDepartment(DepartmentRequest request, ServerCallContext context)
        {
            if (request != null)
            {
                var result = _dbService.AddDepartment(request.Department);
                return Task.FromResult(new StatusResponse { StatusCode = result });
            }
            return Task.FromResult(new StatusResponse { StatusCode = 500 });
        }

        /// <summary>
        /// Receive add employee request from client
        /// </summary>
        /// <param name="request">Employee</param>
        /// <returns>Status code</returns>
        public override Task<StatusResponse> AddEmployee(EmployeeRequest request, ServerCallContext context)
        {
            if (request != null)
            {
                var result = _dbService.AddEmployee(request.Employee);
                return Task.FromResult(new StatusResponse { StatusCode = result });
            }
            return Task.FromResult(new StatusResponse { StatusCode = 500 });
        }

        /// <summary>
        /// Receive remove employee request from client
        /// </summary>
        /// <param name="request">Employee</param>
        /// <returns>Status code</returns>
        public override Task<StatusResponse> RemoveEmployee(EmployeeRequest request, ServerCallContext context)
        {
            if (request != null)
            {
                var result = _dbService.RemoveEmployee(request.Employee);
                return Task.FromResult(new StatusResponse { StatusCode = result });
            }
            return Task.FromResult(new StatusResponse { StatusCode = 500 });
        }

        /// <summary>
        /// Receive update employee request from client
        /// </summary>
        /// <param name="request">Employee</param>
        /// <returns>Status code</returns>
        public override Task<StatusResponse> UpdateEmployee(EmployeeRequest request, ServerCallContext context)
        {
            if (request != null)
            {
                var result = _dbService.UpdateEmployee(request.Employee);
                return Task.FromResult(new StatusResponse { StatusCode = result });
            }
            return Task.FromResult(new StatusResponse { StatusCode = 500 });
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
            var result = _dbService.CheckDbConnection();

            return Task.FromResult(new StatusResponse { StatusCode = result });
        }

    }
}
