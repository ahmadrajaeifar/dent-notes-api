namespace DentalClinic.Contracts.DTOs.Dentists
{
    public class DentistLoginResultDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
