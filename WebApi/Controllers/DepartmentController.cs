using DB.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDbRepository _dbRepository;

        public DepartmentController(IDbRepository dbRepository)
        {
            _dbRepository = dbRepository;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var mapper = new Mapper();
            var departments = _dbRepository.GetDepartments().Select(d => mapper.MapDepartment(d));
            return new JsonResult(departments);
        }

        [HttpPost]
        public JsonResult Post(Models.Department department)
        {
            var mapper = new Mapper();
            var result = _dbRepository.AddDepartment(mapper.MapDepartment(department));
            return new JsonResult(result);
        }
    }
}
