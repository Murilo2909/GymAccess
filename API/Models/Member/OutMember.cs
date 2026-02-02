namespace GymAccess.API.Models.Member
{
    public class OutMember
     {
        public int Id { get; set; }
        public int GymId { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public int CardId { get; set; }
        public string Cpf { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Status { get; set; } = "";
        public string Photo { get; set; } = "";
    }
}