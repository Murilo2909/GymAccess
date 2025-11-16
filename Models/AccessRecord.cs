using Dapper;
using System.Data;

namespace GymAccess.Models
{
    public class AccessRecord
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime AccessTime { get; set; }
        public int GymId { get; set; }

        public static AccessRecord? GetById(IDbConnection connection, int id)
        {
            string query = "SELECT Id, MemberId, AccessTime, GymId FROM AccessRecord WHERE Id = @Id";
            return connection.QueryFirstOrDefault<AccessRecord>(query, new { Id = id });
        }
    }
}
