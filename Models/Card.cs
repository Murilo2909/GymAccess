namespace GymAccess.Models
{
       public class Card
    {
        public int Id { get; set; }
        public string Number { get; set; } = "";
        public int Cvv { get; set; }
        public DateTime Expiration { get; set; }
    }
}
