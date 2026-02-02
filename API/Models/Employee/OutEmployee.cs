namespace GymAccess.API.Models.Employee
{
    public class OutEmployee
     {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public int Admin { get; set; } = 0;
        public int Active { get; set; }
    }
}