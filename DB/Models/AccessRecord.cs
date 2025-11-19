namespace DB.Models
{
    public class AccessRecord
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime AccessTime { get; set; }
        public int GymId { get; set; }
    }
}
