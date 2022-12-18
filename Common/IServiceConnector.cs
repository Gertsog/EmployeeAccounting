namespace Common
{
    public interface IServiceConnector
    {
        public Task<List<Models.Department>> GetDepartmentsAsync();
        public Task<List<Models.Employee>> GetEmployeesAsync();
        public Task<int> AddDepartmentAsync(Models.Department department);
        public Task<int> AddEmployeeAsync(Models.Employee employee);
        public Task<int> RemoveEmployeeAsync(Models.Employee employee);
        public Task<int> UpdateEmployeeAsync(Models.Employee employee);
        public Task<int> GenerateRandomEmployeesAsync();
        public Task<int> CheckConnectionAsync();
        public Task<int> CreateDBAsync();
    }
}
