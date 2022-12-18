namespace GrpcService
{
    public class Mapper
    {
        public Protos.Employee MapEmployee(DB.Data.Employee employee)
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

        public DB.Data.Employee MapEmployee(Protos.Employee employee)
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

        public Protos.Department MapDepartment(DB.Data.Department department)
        {
            return new()
            {
                Id = department.Id,
                Name = department.Name
            };
        }

        public DB.Data.Department MapDepartment(Protos.Department department)
        {
            return new()
            {
                Id = department.Id,
                Name = department.Name
            };
        }
    }
}
