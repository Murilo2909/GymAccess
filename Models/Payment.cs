using Dapper;
using System.Data;

namespace GymAccess.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }

        public static Payment? GetById(IDbConnection connection, int id)
        {
            string query = "SELECT Id, MemberId, Amount, Date, Status FROM Payment WHERE Id = @Id";
            return connection.QueryFirstOrDefault<Payment>(query, new { Id = id });
        }
    }
}
