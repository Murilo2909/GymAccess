namespace DB.Models
{
    public class AccessRecord
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime Time { get; set; }
        public int GymId { get; set; } = 1;
        public int? EmployeeId { get; set; } = null;
    }
}
