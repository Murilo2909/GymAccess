using Dapper;
using System.Data;

namespace GymAccess.Models
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

        public static Member? GetById(IDbConnection connection, int id)
        {
            string query = "SELECT Id, Name, Email, Phone, RegisterDate, GymId FROM Member WHERE Id = @Id";
            return connection.QueryFirstOrDefault<Member>(query, new { Id = id });
        }
    }
}
