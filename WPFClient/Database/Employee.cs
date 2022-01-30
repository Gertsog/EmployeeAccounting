﻿namespace WPFClient.Database
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}