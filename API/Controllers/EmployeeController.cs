using Microsoft.AspNetCore.Mvc;
using GymAccess.API.Models.Employee;
using GymAccess.API.Services;
using DB.Repositories;
using DB.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

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
        [Authorize]
        public async Task<IActionResult> Create([FromBody] InCreateEmployee req)
        {
            if(int.Parse(User.FindFirst("admin")!.Value) != 1)
                return Forbid();
            try
            {
                var employee = new Employee
                {
                    Name = req.Name,
                    Email = req.Email,
                    Password = req.Password,
                    Admin = req.Admin,
                    GymId = int.Parse(User.FindFirst("gymid")!.Value),
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

        [HttpGet("BuscarTodos")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var list = await _repo.GetAll(int.Parse(User.FindFirst("gymid")!.Value));

            List<OutEmployee> response = new();

            foreach (var m in list)
            {
                var dto = new OutEmployee
                {
                    Id = m.Id,
                    Name = m.Name,
                    Email = m.Email,
                    Admin = m.Admin,
                    Active = m.Active
                };
                response.Add(dto);
            }

            return Ok(response);
        }

        [HttpPut("Atualizar/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] InUpdate updated)
        {
            if(int.Parse(User.FindFirst("admin")!.Value) != 1)
                return Forbid();

            var existing = await _repo.GetById(id);

            if (existing == null)
                return NotFound();

            Employee updatedMember = new Employee
            {
                Id = id,
                Name = updated.Name,
                Email = updated.Email,
                Admin = updated.Admin,
                Active = updated.Active,
            };

            var result = await _repo.Update(updatedMember);
            return Ok(result);
        }

        // --------------------------------------------------------
        // 6. DELETAR
        // --------------------------------------------------------
        [HttpDelete("Deletar/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if(int.Parse(User.FindFirst("admin")!.Value) != 1)
                return Forbid();
            var m = _repo.GetById(id);

            if (m == null)
                return NotFound();

            await _repo.Delete(id);

            return Ok("Membro removido.");
        }

    }
}
