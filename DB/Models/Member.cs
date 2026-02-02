using Dapper;
using System.Data;

namespace DB.Models
{
    public class Member
    {
        public int Id { get; set; }
        public int GymId { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public int CardId { get; set; }
        public string Cpf { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Status { get; set; } = "";
        public float[]? FacialFloat { get; set; } = new float[128];
        public string? Facial { get; set; } = "";
        public DateTime PaymentDate { get; set; }
    }
}
