using Dapper;
using DB.Connection;
using DB.Models;

namespace DB.Repositories
{
    public class EmployeeRepository
    {
        private readonly DbConnectionFactory _factory;

        public EmployeeRepository(DbConnectionFactory factory)
        {
            _factory = factory;
        }
         public async Task<int> Insert(Employee emp)
        {
            var sql = @"
                INSERT INTO employee (name, email, password, admin, gymid, active)
                VALUES (@Name, @Email, @Password, @Admin, 1, 1);

                SELECT LAST_INSERT_ID();
            ";

            using var conn = _factory.CreateConnection();
            int newId = await conn.ExecuteScalarAsync<int>(sql, emp);

            return newId;
        }
        public async Task<Employee?> Login(string email, string password)
        {
            using var conn = _factory.CreateConnection();

            string sql = @"SELECT * FROM employee 
                           WHERE Email = @Email AND Password = @Password";

            return await conn.QueryFirstOrDefaultAsync<Employee>(sql, new { Email = email, Password = password });
        }
    }
}