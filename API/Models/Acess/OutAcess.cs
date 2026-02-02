using GymAccess.API.Models.Member;
using GymAccess.API.Models.Employee;

namespace GymAccess.API.Models.Access
{
    public class OutAccess
    {
        public OutMember? Member { get; set; } = new OutMember();
        public String? EmployeeName { get; set; } = "";
        public int MemberId { get; set; }
        public DateTime Time { get; set; }
        public int? EmployeeId { get; set; } = null;
    }
}
