using Dapper;
using DB.Connection;
using DB.Models;

namespace DB.Repositories
{
    public class AccessRepository
    {
        private readonly DbConnectionFactory _factory;

        public AccessRepository(DbConnectionFactory factory)
        {
            _factory = factory;
        }

        // Registrar entrada
        public async Task<AccessRecord> InsertEntryAsync(AccessRecord access)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"
                INSERT INTO AccessHistory
                    (MemberId, GymId, Time)
                VALUES
                    (@MemberId, @GymId, @Time);

                SELECT LAST_INSERT_ID();
            ";

            int newId = await connection.ExecuteScalarAsync<int>(sql, access);

            access.Id = newId;
            return access;
        }
        public async Task<AccessRecord> InsertManulEntryAsync(AccessRecord access)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"
                INSERT INTO AccessHistory
                    (MemberId, GymId, Time, EmployeeId)
                VALUES
                    (@MemberId, @GymId, @Time, @EmployeeId);

                SELECT LAST_INSERT_ID();
            ";

            int newId = await connection.ExecuteScalarAsync<int>(sql, access);

            access.Id = newId;
            return access;
        }

        // Buscar por Id
        public async Task<AccessRecord?> GetByIdAsync(int id)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"SELECT * FROM AccessHistory WHERE Id = @Id";

            return await connection.QueryFirstOrDefaultAsync<AccessRecord>(sql, new { Id = id });
        }

        // Listar todos os acessos
        public async Task<IEnumerable<AccessRecord>> GetAllAsync()
        {
            using var connection = _factory.CreateConnection();

            string sql = @"SELECT * FROM AccessHistory ORDER BY EntryTime DESC";

            return await connection.QueryAsync<AccessRecord>(sql);
        }

        // Listar acessos de um membro
        public async Task<string> GetByEmployeeIdAsync(int id)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"
                SELECT Name FROM Employee
                WHERE Id = @id
            ";

            return await connection.QueryFirstAsync<string>(sql, new { Id = id });
        }
        public async Task<IEnumerable<AccessRecord>> GetByMemberIdAsync(int memberId)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"
                SELECT * FROM AccessHistory
                WHERE MemberId = @MemberId
                ORDER BY id DESC
            ";

            return await connection.QueryAsync<AccessRecord>(sql, new { MemberId = memberId });
        }
        public async Task<IEnumerable<AccessRecord>> GetAll(int GymId)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"
                SELECT *
                FROM AccessHistory
                WHERE GymId = @GymId
                ORDER BY id Desc;
            ";

            return await connection.QueryAsync<AccessRecord>(sql, new { GymId = GymId });
        }
        public async Task<Member?> GetUser(int id)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"
                SELECT 
                    Id,
                    GymId,
                    Name,
                    Email,
                    CardId,
                    Cpf,
                    Phone,
                    Status,
                    PaymentDate
                FROM member
                WHERE Id = @Id;
            ";

            var member = await connection.QueryFirstOrDefaultAsync<Member>(sql, new { Id = id });

            if (member == null)
                return null;

            return member;
        }
    }
}
