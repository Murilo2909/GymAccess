using Microsoft.AspNetCore.Mvc;

namespace GymAccess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GymController : ControllerBase
    {
        [HttpGet("example")]
        public IActionResult Example()
        {
            return Ok("GymController funcionando!");
        }
    }
}
