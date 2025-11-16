using Dapper;
using System.Data;

namespace GymAccess.Models
{
    public class Gym
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";

        public static Gym? GetById(IDbConnection connection, int id)
        {
            string query = "SELECT Id, Name, Address, Phone FROM Gym WHERE Id = @Id";
            return connection.QueryFirstOrDefault<Gym>(query, new { Id = id });
        }
    }
}
