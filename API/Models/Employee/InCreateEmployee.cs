namespace GymAccess.API.Models.Employee
{
    public class InCreateEmployee
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public int Admin { get; set; } = 0;
    }
}
