using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace DB.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public int Admin { get; set; } = 0;
        public int GymId { get; set; }
        public int Active { get; set; }
    }
}
