using Dapper;
using DB.Connection;
using DB.Models;
using System.Globalization;


namespace DB.Repositories
{
    public class MemberRepository
    {
        private readonly DbConnectionFactory _factory;

        public MemberRepository(DbConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<int> Insert(Member member)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"
                INSERT INTO member
                    (GymId, Name, Email, CardId, Cpf, Phone, Status, Facial, PaymentDate)
                VALUES
                    (@GymId, @Name, @Email, @CardId, @Cpf, @Phone, @Status, @Facial, @PaymentDate);

                SELECT LAST_INSERT_ID();
            ";

            try
            {
                int newId = await connection.ExecuteScalarAsync<int>(sql, member);
                Console.WriteLine($"===== Insert Member Success: ID {newId} =====");
                return newId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("===== Insert Member Exception =====");
                Console.WriteLine(ex);
                Console.WriteLine("===================================");
                throw;
            }
        }

        // ------------------------
        // GET BY ID
        // ------------------------
        public async Task<Member?> GetById(int id)
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

        public async Task<Member?> GetByCpf(string Cpf)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"
                SELECT 
                    Id
                FROM member
                WHERE Cpf = @Cpf;
            ";

            var member = await connection.QueryFirstOrDefaultAsync<Member>(sql, new { Cpf = Cpf });
            Console.WriteLine(member == null);
            if (member == null)
                return null;

            return member;
        }
        // ------------------------
        // UPDATE
        // ------------------------
        public async Task<bool> Update(Member member)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"
                UPDATE member
                SET 
                    Name = @Name,
                    Email = @Email,
                    Phone = @Phone,
                    Status = @Status
                WHERE Id = @Id;
            ";

            int rows = await connection.ExecuteAsync(sql, member);
            return rows > 0;
        }

        // ------------------------
        // GET ALL + EMBEDDING JÁ PARSEADO
        // ------------------------
        public async Task<IEnumerable<Member>> GetAllWithEmbedding()
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
                    Facial,
                    PaymentDate
                FROM member
                WHERE Facial IS NOT NULL AND Facial <> '';
            ";

            var results = await connection.QueryAsync<Member>(sql);

            foreach (var m in results)
            {
                if (!string.IsNullOrEmpty(m.Facial))
                {
                    // Substitui vírgula por ponto se necessário, e converte
                    m.FacialFloat = m.Facial
                        .Split('.', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => float.Parse(s.Trim(), CultureInfo.InvariantCulture))
                        .ToArray();
                }
                
            }
            return results;
        }
        public async Task<IEnumerable<Member>> GetAll(int GymId)
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
                WHERE GymId = @GymId;
            ";

            var results = await connection.QueryAsync<Member>(sql, new { GymId = GymId });
            foreach (var m in results)
            {
                m.FacialFloat = null; // Não carregar embedding aqui
            }
            return results;
        }

        public async Task<bool> Delete(int id)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"DELETE FROM member WHERE Id = @Id";

            int rows = await connection.ExecuteAsync(sql, new { Id = id });

            return rows > 0;
        }
    }
}
