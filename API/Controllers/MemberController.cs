using Microsoft.AspNetCore.Mvc;
using DB.Repositories;
using DB.Models;
using GymAccess.API.Services;
using GymAccess.API.Models.Member;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GymAccess.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly MemberRepository _repo;
        private readonly FacialRecognitionService _facial;

        public MemberController(IWebHostEnvironment env, MemberRepository repo, FacialRecognitionService facial)
        {
            _env = env;
            _repo = repo;
            _facial = facial;
        }

        // --------------------------------------------------------
        // 1. REGISTRO DO MEMBRO COM EMBEDDING FACIAL
        // --------------------------------------------------------
        [HttpPost("Register")]
        [Authorize]
        public async Task<IActionResult> Register([FromForm] InRegister memb)
        {
            //verificações
            if (string.IsNullOrWhiteSpace(memb.Name) ||
            string.IsNullOrWhiteSpace(memb.Email) ||
            string.IsNullOrWhiteSpace(memb.Cpf) ||
            string.IsNullOrWhiteSpace(memb.Phone) ||
            string.IsNullOrWhiteSpace(memb.Photo)
            )
                return BadRequest("Não foram informados todos os dados.");

                
            if(await _repo.GetByCpf(memb.Cpf) != null)
                return BadRequest("CPF já cadastrado.");    
                

            byte[]? imageBytes = null;

            // 1. Converter imagem Base64 (se existir)
            if (!string.IsNullOrWhiteSpace(memb.Photo))
            {
                string base64 = memb.Photo;

                if (base64.StartsWith("data:image"))
                {
                    int idx = base64.IndexOf("base64,") + 7;
                    base64 = base64.Substring(idx);
                }

                try
                {
                    imageBytes = Convert.FromBase64String(base64);
                }
                catch
                {
                    return BadRequest("Imagem em Base64 inválida.");
                }
            }

            // 2. Extrair embedding
            string facialString = "";
            if (imageBytes != null)
            {
                var emb = _facial.ExtractEmbedding(imageBytes);
                if (emb == null || emb.Length == 0)
                    return BadRequest("Nenhum rosto detectado.");
                facialString = string.Join(".", emb);
            }

            // 3. Criar objeto Member (sem foto)
            var member = new Member
            {
                GymId = int.Parse(User.FindFirst("gymid")!.Value),
                Name = memb.Name,
                Email = memb.Email,
                CardId = memb.CardId,
                Cpf = memb.Cpf,
                Phone = memb.Phone,
                Status = memb.Status,
                Facial = facialString,
                PaymentDate = DateTime.Now
            };

            // 4. Inserir no banco primeiro (gera o ID)
            int? inserted = await _repo.Insert(member);
            if (inserted == null || inserted <= 0)
                return BadRequest("Erro ao inserir o membro.");

            // 5. Salvar a foto no disco agora usando o ID
            string? photoUrl = null;

            if (imageBytes != null)
            {
                string folder = Path.Combine(_env.ContentRootPath, "wwwroot", "images", "members");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                Console.WriteLine($"Saving member photo to {inserted}.jpg");

                string fileName = $"{inserted}.jpg";
                string fullPath = Path.Combine(folder, fileName);

                System.IO.File.WriteAllBytes(fullPath, imageBytes);

                // Caminho público da foto
                photoUrl = $"/images/members/{fileName}";
            }

            // 6. Retornar tudo ao cliente
            return Ok(new
            {
                Message = "Membro registrado com sucesso.",
                PhotoUrl = photoUrl,
                Member = inserted
            });
        }


        // --------------------------------------------------------
        // 3. LISTAR TODOS
        // --------------------------------------------------------
        [HttpGet("BuscarTodos")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var list = await _repo.GetAll(int.Parse(User.FindFirst("gymid")!.Value));

            List<OutMember> response = new();

            foreach (var m in list)
            {
                var dto = new OutMember
                {
                    Id = m.Id,
                    GymId = m.GymId,
                    Name = m.Name,
                    Email = m.Email,
                    CardId = m.CardId,
                    Cpf = m.Cpf,
                    Phone = m.Phone,
                    Status = m.Status
                };
                string basePath = Path.Combine(_env.ContentRootPath, "wwwroot", "images", "members");
                // Buscar foto {id}.jpg
                string photoPath = Path.Combine(basePath, $"{m.Id}.jpg");

                if (System.IO.File.Exists(photoPath))
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(photoPath);
                    string b64 = Convert.ToBase64String(bytes);

                    dto.Photo = $"data:image/jpg;base64,{b64}";
                }
                else
                {
                    dto.Photo = ""; // sem foto
                }
                response.Add(dto);
            }

            return Ok(response);
        }

        // --------------------------------------------------------
        // 4. BUSCAR POR ID
        // --------------------------------------------------------
        [HttpGet("Buscar/{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var m = await _repo.GetById(id);
            if (m == null)
                return NotFound();


            var dto = new OutMember
            {
                Id = m.Id,
                GymId = m.GymId,
                Name = m.Name,
                Email = m.Email,
                CardId = m.CardId,
                Cpf = m.Cpf,
                Phone = m.Phone,
                Status = m.Status,
            };

            string basePath = Path.Combine(_env.ContentRootPath, "wwwroot", "images", "members");
            // Buscar foto {id}.jpg
            string photoPath = Path.Combine(basePath, $"{m.Id}.jpg");

            if (System.IO.File.Exists(photoPath))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(photoPath);
                string b64 = Convert.ToBase64String(bytes);

                dto.Photo = $"data:image/jpg;base64,{b64}";
            }
            else
            {
                dto.Photo = ""; // sem foto
            }

            return Ok(dto);
        }

        // --------------------------------------------------------
        // 5. ATUALIZAR MEMBRO
        // --------------------------------------------------------
        [HttpPut("Atualizar/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] InUpdate updated)
        {
            if(int.Parse(User.FindFirst("admin")!.Value) != 1)
                return Forbid();

            var existing = await _repo.GetById(id);

            if (existing == null)
                return NotFound();

            Member updatedMember = new Member
            {
                Id = id,
                GymId = existing.GymId,
                Name = updated.Name,
                Email = updated.Email,
                CardId = existing.CardId,
                Cpf = existing.Cpf,
                Phone = updated.Phone,
                Status = updated.Status,
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
