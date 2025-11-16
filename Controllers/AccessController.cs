using Microsoft.AspNetCore.Mvc;

namespace GymAccess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessController : ControllerBase
    {
        [HttpGet("example")]
        public IActionResult Example()
        {
            return Ok("AccessController funcionando!");
        }
    }
}
