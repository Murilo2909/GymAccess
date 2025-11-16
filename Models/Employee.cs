using Dapper;
using System.Data;

namespace GymAccess.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Position { get; set; } = "";
        public DateTime HireDate { get; set; }
        public int GymId { get; set; }

        public static Employee? GetById(IDbConnection connection, int id)
        {
            string query = "SELECT Id, Name, Position, HireDate, GymId FROM Employee WHERE Id = @Id";
            return connection.QueryFirstOrDefault<Employee>(query, new { Id = id });
        }
    }
}
