using Microsoft.EntityFrameworkCore;

namespace WPFClient.Database
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "MyDbContext" в коде и файле конфигурации.
    public class EmployeeAccountingDbContext : DbContext
    {
        private string sqlServerName = "LAPTOP-OQ8825HM\\SQLEXPRESS";
        private string dbName = "EmployeeAccounting";
        
        public EmployeeAccountingDbContext()
        {
        }

        public EmployeeAccountingDbContext(DbContextOptions<EmployeeAccountingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer($"Server = {sqlServerName}; Database = {dbName}; Trusted_Connection = True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasIndex(d => d.Name)
                .IsUnique();
        }
    }

}
