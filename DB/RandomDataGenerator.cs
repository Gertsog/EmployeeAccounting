using DB.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace DB
{
    // Класс для генерации случайных сотрудников с помощью API
    public class RandomDataGenerator
    {
        readonly SqlDbContext _db;
        readonly string[] positions = { "Разработчик", "Девопс", "Бухгалтер" };
        readonly string[] departmentNames = { "Разработка", "Сопровождение", "Бухгалтерия" };
        readonly string url = "https://api.randomdatatools.ru/";
        readonly string requestParams = "?count=50&gender=unset&typeName=all&unescaped=false";
        readonly string dataParams = "&params=LastName,FirstName,FatherName";

        public RandomDataGenerator(SqlDbContext context)
        {
            _db = context;
        }

        //Создание заготовленных заранее отделов
        public void GenerateDepartments()
        {
            _db.Departments.Load();
            var newDepartments = new List<Department>();
            foreach (var departmentName in departmentNames)
            {
                if (_db.Departments.Any(d => d.Name == departmentName) == false)
                {
                    newDepartments.Add(new Department { Name = departmentName });
                }
            }
            if (newDepartments.Count > 0)
            {
                _db.Departments.AddRange(newDepartments);
                _db.SaveChanges();
            }
        }

        //Получение сотрудников с помощью API и дозаполнение отдела, должности, оклада
        public async Task GenerateRandomEmployeesAsync()
        {
            GenerateDepartments();

            var client = new HttpClient();
            var response = await client.GetAsync(url + requestParams + dataParams);
            var employees = new List<Employee>();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                employees = JsonSerializer.Deserialize<List<Employee>>(result);
            }

            for (int i = 0; i < employees.Count; i++)
            {
                var random = new Random();
                int randomInt = random.Next(0, 15);
                int index = (randomInt % 5 == 0) ? 2 : (randomInt % 2);
                employees[i].DepartmentId = _db.Departments.First(d => d.Name == departmentNames[index]).Id;
                employees[i].Position = positions[index];
                employees[i].Salary = (decimal)random.NextDouble() * (120000 - 30000) + 30000;
            }

            _db.AddRange(employees);
            _db.SaveChanges();
        }
    }
}
