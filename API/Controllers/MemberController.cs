using Microsoft.AspNetCore.Mvc;

namespace GymAccess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        [HttpGet("example")]
        public IActionResult Example()
        {
            return Ok("MemberController funcionando!");
        }
    }
}
