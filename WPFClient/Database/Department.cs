using System.Collections.Generic;

namespace WPFClient.Database
{
    public class Department
    {
        public ulong Id { get; set; }
        public string Name { get; set; } 

        public virtual ICollection<Employee> Employees { get; set; }

    }
}
