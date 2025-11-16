using Microsoft.AspNetCore.Mvc;

namespace GymAccess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        [HttpGet("example")]
        public IActionResult Example()
        {
            return Ok("EmployeeController funcionando!");
        }
    }
}
