namespace GymAccess.API.Models.Member
{
    public class InUpdate
    {
        int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Status { get; set; } = "";
    }
}