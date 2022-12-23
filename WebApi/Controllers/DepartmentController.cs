using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DbService _dbService;

        public DepartmentController(DbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var result = _dbService.GetDepartments();

            return new JsonResult(result);
        }

        [HttpPost]
        public JsonResult Post(Models.Department department)
        {
            var result = _dbService.AddDepartment(department);

            return new JsonResult(result);
        }
    }
}
