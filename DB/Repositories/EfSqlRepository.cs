using DB.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DB.Repositories
{
    //TODO: проверить работу с PostgreSQL
    public class EfSqlRepository : IDbRepository
    {
        private string _connectionString;

        public EfSqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Add department to database
        /// </summary>
        /// <param name="department">Department</param>
        /// <returns>Status code</returns>
        public int AddDepartment(Department department)
        {
            using var db = new SqlDbContext(_connectionString);
            if (department != null && !DepartmentAlreadyExists(department))
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return (int)HttpStatusCode.OK;
            }
            return (int)HttpStatusCode.InternalServerError;
        }

        public int AddEmployee(Employee employee)
        {
            using var db = new SqlDbContext(_connectionString);
            if (employee != null && !EmployeeAlreadyExists(employee))
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return (int)HttpStatusCode.OK;
            }
            return (int)HttpStatusCode.InternalServerError;
        }

        public int CheckDBConnection()
        {
            using var db = new SqlDbContext(_connectionString);
            if (db.Database.CanConnect())
                return (int)HttpStatusCode.OK;
            return (int)HttpStatusCode.InternalServerError;
        }

        public int CreateDB()
        {
            using var db = new SqlDbContext(_connectionString);
            if (!db.Database.CanConnect())
                db.Database.EnsureCreated();
            if (db.Database.CanConnect())
                return (int)HttpStatusCode.OK;
            return (int)HttpStatusCode.InternalServerError;
        }

        public int GenerateRandomEmployees()
        {
            try
            {
                using var db = new SqlDbContext(_connectionString);
                var generator = new RandomDataGenerator(db);
                generator.GenerateRandomEmployeesAsync();
                return (int)HttpStatusCode.OK;
            }
            catch { }
            return (int)HttpStatusCode.InternalServerError;
        }

        public List<Department> GetDepartments()
        {
            using var db = new SqlDbContext(_connectionString);
            return db.Departments.ToList();
        }

        public List<Employee> GetEmployees()
        {
            using var db = new SqlDbContext(_connectionString);
            return db.Employees.Include(e => e.Department).ToList();
        }

        public int RemoveEmployee(Employee employee)
        {
            using var db = new SqlDbContext(_connectionString);
            if (employee != null && employee.Id != default && EmployeeAlreadyExists(employee))
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
                return (int)HttpStatusCode.OK;
            }
            return (int)HttpStatusCode.InternalServerError;
        }

        public int UpdateEmployee(Employee employee)
        {
            using var db = new SqlDbContext(_connectionString);
            if (employee != null && employee.Id != default)
            {
                db.Employees.Update(employee);
                db.SaveChanges();
                return (int)HttpStatusCode.OK;
            }
            return (int)HttpStatusCode.InternalServerError;
        }

        private bool DepartmentAlreadyExists(Department department)
        {
            using var db = new SqlDbContext(_connectionString);
            return db.Departments.Any(d => d.Name == department.Name);
        }

        //А нужна ли эта проверка?
        private bool EmployeeAlreadyExists(Employee employee)
        {
            using var db = new SqlDbContext(_connectionString);
            return db.Employees.Any(e =>
                e.FirstName == employee.FirstName &&
                e.LastName == employee.LastName &&
                e.FatherName == employee.FatherName &&
                e.Position == employee.Position &&
                e.DepartmentId == employee.DepartmentId
            );
        }
    }
}
