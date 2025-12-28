using DentalClinic.Contracts.DTOs.Dentists;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;


namespace DentalClinic.Web.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var loginDto = new DentistLoginDto
            {
                Username = Username,
                Password = Password
            };

            var content = new StringContent(
                JsonSerializer.Serialize(loginDto),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(
                "https://localhost:53027/api/dentist/login",
                content);

            if (!response.IsSuccessStatusCode)
            {
                ErrorMessage = "نام کاربری یا رمز عبور اشتباه است";
                return Page();
            }

            var json = await response.Content.ReadAsStringAsync();
            var loginResult = JsonSerializer.Deserialize<LoginResultDto>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (loginResult == null)
            {
                ErrorMessage = "خطای سرور!";
                return Page();
            }

            // ذخیره JWT در Cookie
            Response.Cookies.Append(
                "access_token",
                loginResult!.Token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

            return RedirectToPage("/Patients/Index");
        }
    }
}
