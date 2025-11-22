namespace GymAccess.API.Models.Member
{
    public class InRegister
    {
        public int GymId { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public int CardId { get; set; }
        public string Cpf { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Status { get; set; } = "";

        // Facial recebida da API
        public float[] Facial { get; set; } = new float[128];

        // Envio da imagem
        public IFormFile? Photo { get; set; }
    }
}