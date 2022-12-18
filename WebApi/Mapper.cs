namespace WebApi
{
    public class Mapper
    {
        public Models.Department MapDepartment(DB.Data.Department department)
        {
            return new()
            {
                Id = department.Id,
                Name = department.Name
            };
        }

        public DB.Data.Department MapDepartment(Models.Department department)
        {
            return new()
            {
                Id = department.Id,
                Name = department.Name
            };
        }

        public Models.Employee MapEmployee(DB.Data.Employee employee)
        {
            return new()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                FatherName = employee.FatherName,
                Position = employee.Position,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department.Name
            };
        }

        public DB.Data.Employee MapEmployee(Models.Employee employee)
        {
            return new()
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                FatherName = employee.FatherName,
                Position = employee.Position,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId
            };
        }
    }
}
