using Microsoft.EntityFrameworkCore;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace WPFClient.Database
{
    // Класс для генерации случайных сотрудников с помощью API
    public class RandomDataGenerator
    {
        readonly EmployeeAccountingDbContext db;

        string[] positions = { "Разработчик", "Девопс", "Бухгалтер" };
        string[] departmentNames = { "Разработка", "Сопровождение", "Бухгалтерия" };

        string url = "https://api.randomdatatools.ru/";
        string requestParams = "?count=50&gender=unset&typeName=all&unescaped=false";
        string dataParams = "&params=LastName,FirstName,FatherName";

        public RandomDataGenerator(EmployeeAccountingDbContext context) 
        {
            db = context;
        }

        //Создание заготовленных заранее отделов
        public void GenerateDepartments()
        {
            db.Departments.Load();
            var newDepartments = new List<Department>();
            foreach (var departmentName in departmentNames) 
            {
                if (!db.Departments.Any(d => d.Name == departmentName)) 
                {
                    newDepartments.Add(new Department() { Name = departmentName });
                }
            }
            if (newDepartments.Count > 0) 
            {
                db.Departments.AddRange(newDepartments);
                db.SaveChanges();
            }
        }

        //Получение сотрудников с помощью API и дозаполнение отдела, должности, оклада
        public void GenerateRandomEmployees()
        {
            GenerateDepartments();

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

            for (int i = 0; i < employees.Count; i++)
            {
                var random = new Random();
                int randomInt = random.Next(0, 15);
                int index = (randomInt % 5 == 0) ? 2 : randomInt % 2;
                employees[i].DepartmentId = db.Departments.First(d => d.Name == departmentNames[index]).Id;
                employees[i].Position = positions[index];
                employees[i].Salary = random.NextDouble() * (120000 - 30000) + 30000;
            }

            db.AddRange(employees);
            db.SaveChanges();
        }
    }
}
