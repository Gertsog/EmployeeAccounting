using Common.Models;
using System.Net.Http.Json;

namespace ServiceConnector
{
    public class WebApiConnector : IServiceConnector
    {
        private HttpClient _httpClient;
        private string _url;

        public WebApiConnector(string serviceUrl)
        {
            _url = serviceUrl;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_url)
            };
        }

        ~WebApiConnector()
        {
            _httpClient.Dispose();
        }

        public async Task<int> AddDepartmentAsync(Department department)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Department", department);
            var result = await response.Content.ReadFromJsonAsync<int>();

            return result;
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Employee", employee);
            var result = await response.Content.ReadFromJsonAsync<int>();

            return result;
        }

        //Избавиться бы от этого
        public async Task<int> CheckDbConnectionAsync()
        {
            var response = await _httpClient.PostAsync("/api/Employee/CheckConnection", null);
            var result = await response.Content.ReadFromJsonAsync<int>();

            return result;
        }

        public async Task<int> GenerateRandomEmployeesAsync()
        {
            var response = await _httpClient.PostAsync("/api/Employee/AddRandomEmployees", null);
            var result = await response.Content.ReadFromJsonAsync<int>();

            return result;
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            var response = await _httpClient.GetAsync("/api/Department");
            var result = await response.Content.ReadFromJsonAsync<List<Department>>();

            result ??= new();

            return result;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("/api/Employee");
            var result = await response.Content.ReadFromJsonAsync<List<Employee>>();

            result ??= new();

            return result;
        }

        public async Task<int> RemoveEmployeeAsync(Employee employee)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(employee),
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_url + "/api/Employee", UriKind.Relative)
            };
            var response = await _httpClient.SendAsync(request);
            var result = await response.Content.ReadFromJsonAsync<int>();

            return result;
        }

        public async Task<int> UpdateEmployeeAsync(Employee employee)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/Employee", employee);
            var result = await response.Content.ReadFromJsonAsync<int>();

            return result;
        }
    }

}
