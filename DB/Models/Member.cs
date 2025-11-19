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
        public float[] Facial { get; set; } = new float[128];
        public DateTime PaymentDate { get; set; }
    }
}
