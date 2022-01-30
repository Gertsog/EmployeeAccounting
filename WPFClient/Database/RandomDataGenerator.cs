using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;


namespace WPFClient.Database
{
    public class RandomDataGenerator
    {
        string url = "https://api.randomdatatools.ru/";
        string requestParams = "?count=50&gender=unset&typeName=all&unescaped=false";
        string dataParams = "&params=LastName,FirstName,FatherName";
        string[] positions = { "Разработчик", "Девопс", "Бухгалтер" };
        string[] departments = { "Разработка", "Сопровождение", "Бухгалтерия" };

        public RandomDataGenerator() 
        {
        }

        public List<Employee> GenerateRandomEmployees() 
        {
            var request = WebRequest.Create(url + requestParams + dataParams);
            var response = request.GetResponse();
            string responseStirng;
            var employees = new List<Employee>();

            using (Stream dataStream = response.GetResponseStream())
            {
                var js = new JavaScriptSerializer();
                responseStirng = new StreamReader(dataStream).ReadToEnd();
                employees = js.Deserialize<List<Employee>>(responseStirng);
            }
            response.Close();

            for (int i = 0; i < employees.Count(); i++)
            {
                var random = new Random();
                int randomInt = random.Next(0, 15);
                int index = (randomInt % 5 == 0) ? 2 : randomInt % 2;
                employees[i].Department = new Department() { Name = departments[index] };
                employees[i].Position = positions[index];
                employees[i].Salary = random.NextDouble() * (120000 - 30000) + 30000;
            }

            return employees;
        }

        public void FillDbWithEmployees(EmployeeAccountingDbContext context, IEnumerable<Employee> employees)
        {
            var departments = employees.Select(e => e.Department).ToList();
            var departmentsNames = departments.Select(d => d.Name).Distinct().ToList();
            var newDepartments = departmentsNames.Where(d => !context.Departments.Select(dd => dd.Name).Contains(d));

            context.AddRange(newDepartments.Select(nd => new Department() { Name = nd }));
            context.SaveChanges();

            for (int i = 0; i < employees.Count(); i++)
            {
                employees.ElementAt(i).DepartmentId = context.Departments.First(d => d.Name == employees.ElementAt(i).Department.Name).Id;
                employees.ElementAt(i).Department = null;
            }
            context.AddRange(employees);
            context.SaveChanges();
        }
    }
}
