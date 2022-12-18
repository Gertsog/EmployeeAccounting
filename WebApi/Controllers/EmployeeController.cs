using DB.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IDbRepository _dbRepository;

        public EmployeeController(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var employees = _dbRepository.GetEmployees();
            return new JsonResult(employees);
        }

        [HttpPost]
        public JsonResult Post(Models.Employee employee)
        {
            var mapper = new Mapper();
            var result = _dbRepository.AddEmployee(mapper.MapEmployee(employee));
            return new JsonResult(result);
        }

        [HttpPut]
        public JsonResult Put(Models.Employee employee)
        {
            var mapper = new Mapper();
            var result = _dbRepository.UpdateEmployee(mapper.MapEmployee(employee));
            return new JsonResult(result);
        }

        //[HttpDelete("{id}")]
        [HttpDelete]
        public JsonResult Delete(Models.Employee employee)
        {
            var mapper = new Mapper();
            var result = _dbRepository.RemoveEmployee(mapper.MapEmployee(employee));
            return new JsonResult(result);
        }
    }
}
