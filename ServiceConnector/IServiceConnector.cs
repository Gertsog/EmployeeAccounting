namespace ServiceConnector
{
    //TODO: реализовать WebApiConnector
    public interface IServiceConnector
    {
        public Task<List<Common.Models.Department>> GetDepartmentsAsync();
        public Task<List<Common.Models.Employee>> GetEmployeesAsync();
        public Task<int> AddDepartmentAsync(Common.Models.Department department);
        public Task<int> AddEmployeeAsync(Common.Models.Employee employee);
        public Task<int> RemoveEmployeeAsync(Common.Models.Employee employee);
        public Task<int> UpdateEmployeeAsync(Common.Models.Employee employee);
        public Task<int> GenerateRandomEmployeesAsync();
        public Task<int> CheckConnectionAsync();
        public Task<int> CreateDBAsync();
    }
}
