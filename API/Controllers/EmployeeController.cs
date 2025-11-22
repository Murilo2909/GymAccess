using Microsoft.AspNetCore.Mvc;
using GymAccess.API.Models.Employee;
using GymAccess.API.Services;
using DB.Repositories;
using DB.Models;

namespace GymAccess.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _repo;
        private readonly JwtService _jwtService;

        public EmployeeController(EmployeeRepository repo, JwtService jwtService)
        {
            _repo = repo;
            _jwtService = jwtService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Create([FromBody] InCreateEmployee req)
        {
            try
            {
                var employee = new Employee
                {
                    Name = req.Name,
                    Email = req.Email,
                    Password = req.Password,
                    Admin = req.Admin
                };

                int id = await _repo.Insert(employee);

                return Ok(new { Id = id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] InLogin dados)
        {
            var user = await _repo.Login(dados.Email, dados.Password);

            if (user == null)
                return Unauthorized("Usuário ou senha inválidos.");

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                token = token,
                admin = user.Admin,
                gymid = user.GymId,
                username = user.Name
            });
        }
    }
}
