namespace DentalClinic.Api.DTOs.Common
{
    public class RefreshTokenDto
    {
        public string Username { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
