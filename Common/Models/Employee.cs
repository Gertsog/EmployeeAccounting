namespace Common.Models
{
    public class Employee
    {
        public ulong Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }

        public ulong DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
