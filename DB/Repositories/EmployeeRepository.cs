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
                VALUES (@Name, @Email, @Password, @Admin, @GymId, 1);

                SELECT LAST_INSERT_ID();
            ";

            using var conn = _factory.CreateConnection();
            int newId = await conn.ExecuteScalarAsync<int>(sql, emp);

            return newId;
        }

        public async Task<Employee?> GetById(int id)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"
                SELECT *
                FROM Employee
                WHERE Id = @Id;
            ";

            var emp = await connection.QueryFirstOrDefaultAsync<Employee>(sql, new { Id = id });

            if (emp == null)
                return null;

            return emp;
        }
        public async Task<Employee?> Login(string email, string password)
        {
            using var conn = _factory.CreateConnection();

            string sql = @"SELECT * FROM employee 
                           WHERE Email = @Email AND Password = @Password";

            return await conn.QueryFirstOrDefaultAsync<Employee>(sql, new { Email = email, Password = password });
        }
        public async Task<IEnumerable<Employee>> GetAll(int GymId)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"
                SELECT *
                FROM Employee
                WHERE GymId = @GymId;
            ";

            return await connection.QueryAsync<Employee>(sql, new { GymId = GymId });
        }

        public async Task<bool> Update(Employee emp)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"
                UPDATE Employee
                SET 
                    Name = @Name,
                    Email = @Email,
                    Admin = @Admin,
                    Active = @Active
                WHERE Id = @Id;
            ";

            int rows = await connection.ExecuteAsync(sql, emp);
            return rows > 0;
        }

        public async Task<bool> Delete(int id)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"DELETE FROM Employee WHERE Id = @Id";

            int rows = await connection.ExecuteAsync(sql, new { Id = id });

            return rows > 0;
        }
    }
}