using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DbService _dbService;

        public EmployeeController(DbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var result = _dbService.GetEmployees();

            return new JsonResult(result);
        }

        [HttpPost]
        public JsonResult Post(Models.Employee employee)
        {
            var result = _dbService.AddEmployee(employee);

            return new JsonResult(result);
        }

        [HttpPut]
        public JsonResult Put(Models.Employee employee)
        {
            var result = _dbService.UpdateEmployee(employee);

            return new JsonResult(result);
        }

        //[HttpDelete("{id}")]
        [HttpDelete]
        public JsonResult Delete(Models.Employee employee)
        {
            var result = _dbService.RemoveEmployee(employee);

            return new JsonResult(result);
        }

        [HttpPost]
        [Route("AddRandomEmployees")]
        public JsonResult AddRandomEmployees()
        {
            var result = _dbService.GenerateRandomEmployees();

            return new JsonResult(result);
        }

        [HttpPost]
        [Route("CheckConnection")]
        public JsonResult CheckConnection()
        {
            var result = _dbService.CheckDbConnection();

            return new JsonResult(result);
        }

    }
}
