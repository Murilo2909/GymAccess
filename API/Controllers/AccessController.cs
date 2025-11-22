using Microsoft.AspNetCore.Mvc;
using GymAccess.API.Models.Member;
using DB.Repositories;
using DB.Models;

namespace GymAccess.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessController : ControllerBase
    {

        // public AccessController(MemberRepository repo)
        // {
        //     _repo = repo;
        // }


        // [HttpPost("Register")]
        // public async Task<IActionResult> Register([FromBody] InRegisterAccess req)
        // {
        //     try
        //     {
        //         var access = new AccessRecord
        //         {
        //             MemberId = req.MemberId,
        //             EmployeeId = req.EmployeeId,
        //             AccessTime = DateTime.UtcNow,
        //             Type = req.Type
        //         };

        //         int id = await _repo.Insert(access);

        //         return Ok(new { Id = id, Message = "Acesso registrado com sucesso." });
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        // [HttpPost("Acess")]
        // public async Task<IActionResult> Compare([FromBody] InAccess req)
        // {
        //     try
        //     {
        //         var member = await _repo.GetMemberById(req.MemberId);

        //         if (member == null)
        //             return NotFound("Membro não encontrado.");

        //         bool isActive = member.IsActive && member.MembershipEndDate >= DateTime.UtcNow;

        //         if (!isActive)
        //             return Unauthorized(new { Message = "Acesso negado. Mensalidade expirada ou inativa." });

        //         return Ok(new
        //         {
        //             Message = "Acesso permitido.",
        //             MemberId = member.Id,
        //             MemberName = member.Name,
        //             MembershipEndDate = member.MembershipEndDate,
        //             IsActive = isActive
        //         });
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }
    }
}
