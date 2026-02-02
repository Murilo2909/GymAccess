using Microsoft.AspNetCore.Mvc;
using GymAccess.API.Models.Member;
using GymAccess.API.Models.Access;
using DB.Repositories;
using DB.Models;
using GymAccess.API.Services;
using Microsoft.AspNetCore.Authorization;
using GymAccess.Controllers;

namespace GymAccess.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessController : ControllerBase
    {

        private readonly IWebHostEnvironment _env;
        private readonly MemberRepository _memberRepo;
        private readonly AccessRepository _repo;
        private readonly FacialRecognitionService _faceService;

        public AccessController(
            MemberRepository memberRepo,
            AccessRepository accessRepo,
            FacialRecognitionService faceService,
            IWebHostEnvironment env)
        {
            _env = env;
            _memberRepo = memberRepo;
            _repo = accessRepo;
            _faceService = faceService;
        }

        // Recebe uma imagem em Base64 e identifica o membro
        [HttpPost("VerifyFace")]
        public async Task<IActionResult> VerifyFace([FromForm] string base64Photo)
        {
            byte[] imageBytes;
            if (string.IsNullOrWhiteSpace(base64Photo))
                return BadRequest("Envie uma imagem Base64.");

            // Remove prefixo "data:image/..." se existir
            if (base64Photo.StartsWith("data:image"))
            {
                int idx = base64Photo.IndexOf("base64,") + 7;
                base64Photo = base64Photo.Substring(idx);
            }

            try
            {
                imageBytes = Convert.FromBase64String(base64Photo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao converter Base64: " + ex.Message);
                return BadRequest("Imagem inválida ou corrompida.");
            }

            var inputEmbedding = _faceService.ExtractEmbedding(imageBytes);
            if (inputEmbedding == null)
                return BadRequest("Nenhum rosto detectado na imagem.");

            // Buscar todos os membros com embedding
            var members = await _memberRepo.GetAllWithEmbedding();

            double bestScore = -1;
            Member? bestMatch = null;

            foreach (var m in members)
            {
                double similarity = _faceService.Compare(inputEmbedding, m.FacialFloat);

                if (similarity > bestScore)
                {
                    bestScore = similarity;
                    bestMatch = m;
                }
            }

            Console.WriteLine($"Best Score: {bestScore}");

            if (bestMatch == null || bestScore < 0.25) // Threshold recomendado para ArcFace
                return Unauthorized("Rosto não reconhecido.");

            if(bestMatch.Status != "Ativo")
                return Unauthorized("Membro com pendências ou inativo.");

            // -----------------------------
            // Registrar acesso no banco
            // -----------------------------
            var access = new AccessRecord
            {
                MemberId = bestMatch.Id,
                Time = DateTime.Now,
                GymId = int.Parse(User.FindFirst("gymid")!.Value)
            };
            await _repo.InsertEntryAsync(access);

            bestMatch.FacialFloat = null; // Não retornar embedding
            bestMatch.Facial = null; // Não retornar embedding
            return Ok(new
            {
                Message = "Acesso permitido.",
                Member = bestMatch,
                Similarity = bestScore,
                Access = access
            });
        }

        [HttpPost("AuthorizeManual")]
        [Authorize]
        public async Task<IActionResult> Manual()
        {
            var access = new AccessRecord
            {
                MemberId = 1,
                Time = DateTime.Now,
                EmployeeId = int.Parse(User.FindFirst("id")!.Value),
                GymId = int.Parse(User.FindFirst("gymid")!.Value)
            };

            await _repo.InsertManulEntryAsync(access);

            return Ok(new
            {
                Message = "Acesso permitido.",
                Access = access
            });
        }

        [HttpGet("History")]
        [Authorize]
        public async Task<IActionResult> History()
        {
            var list = await _repo.GetAll(int.Parse(User.FindFirst("gymid")!.Value));

            List<OutAccess> response = new();

            foreach (var a in list)
            {
                OutMember? member = null;
                string? empName = null;

                if(a.EmployeeId == null)
                {
                    var m = await _repo.GetUser(a.MemberId);
                    if (m == null)
                        return NotFound();


                    member = new OutMember
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

                        member.Photo = $"data:image/jpg;base64,{b64}";
                    }
                    else
                    {
                        member.Photo = ""; // sem foto
                    }
                }
                else
                {
                    empName = _repo.GetByEmployeeIdAsync(a.EmployeeId.Value).Result;
                }

                var dto = new OutAccess
                {
                    Member = member,
                    Time = a.Time,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = empName
                };
                response.Add(dto);
            }

            return Ok(response);
        }
    }
}
