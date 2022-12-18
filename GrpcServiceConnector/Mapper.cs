namespace GrpcServiceConnector
{
    public class Mapper
    {
        public Common.Models.Department MapDepartment(Protos.Department department)
        {
            return new()
            {
                Id = department.Id,
                Name = department.Name
            };
        }

        public Protos.Department MapDepartment(Common.Models.Department department)
        {
            return new()
            {
                Id = department.Id,
                Name = department.Name
            };
        }

        public Common.Models.Employee MapEmployee(Protos.Employee employee)
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
                DepartmentName = employee.DepartmentName
            };
        }

        public Protos.Employee MapEmployee(Common.Models.Employee employee)
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
                DepartmentName = employee.DepartmentName
            };
        }
    }
}
