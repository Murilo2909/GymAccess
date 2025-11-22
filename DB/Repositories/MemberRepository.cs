using Dapper;
using DB.Connection;
using DB.Models;

namespace DB.Repositories
{
    public class MemberRepository
    {
        private readonly DbConnectionFactory _factory;

        public MemberRepository(DbConnectionFactory factory)
        {
            _factory = factory;
        }
        public async Task<Member> Insert(Member member)
        {
            using var connection = _factory.CreateConnection();

            string sql = @"
                INSERT INTO Members
                    (GymId, Name, Email, CardId, Cpf, Phone, Status, Facial, PaymentDate)
                VALUES
                    (@GymId, @Name, @Email, @CardId, @Cpf, @Phone, @Status, @Facial, @PaymentDate);

                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            var id = connection.QuerySingle<int>(sql, new
            {
                member.GymId,
                member.Name,
                member.Email,
                member.CardId,
                member.Cpf,
                member.Phone,
                member.Status,
                Facial = string.Join(",", member.Facial),
                member.PaymentDate
            });

            member.Id = id;
            return member;
        }
        
    }
}