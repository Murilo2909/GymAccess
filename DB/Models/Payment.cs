using Dapper;
using System.Data;

namespace DB.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
    }
}
