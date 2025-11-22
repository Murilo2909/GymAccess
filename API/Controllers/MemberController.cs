using Microsoft.AspNetCore.Mvc;
using DB.Repositories;
using DB.Models;
using GymAccess.API.Models.Member;

namespace GymAccess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly MemberRepository _repo;
        private readonly IWebHostEnvironment _env;

        public MemberController(MemberRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        [HttpPost]
        public IActionResult Create([FromForm] InRegister dto)
        {
            // 1. Salvar imagem se enviada
            string? photoPath = null;

            if (dto.Photo != null)
            {
                string folder = Path.Combine(_env.ContentRootPath, "wwwroot", "members");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.Photo.FileName)}";

                string fullPath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                    dto.Photo.CopyTo(stream);

                photoPath = $"/members/{fileName}";
            }

            // 2. Criar objeto Member
            var member = new Member
            {
                GymId = dto.GymId,
                Name = dto.Name,
                Email = dto.Email,
                CardId = dto.CardId,
                Cpf = dto.Cpf,
                Phone = dto.Phone,
                Status = dto.Status,
                Facial = dto.Facial,
                PaymentDate = DateTime.Now
            };

            // Se quiser salvar a foto no banco, adicione aqui depois na Model

            // 3. Inserir no banco
            var inserted = _repo.Insert(member);

            // 4. Retornar com foto
            return Ok(new
            {
                Message = "Membro criado com sucesso.",
                Member = inserted,
                Photo = photoPath
            });
        }
    }
}
