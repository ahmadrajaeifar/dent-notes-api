using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace DentalClinic.Web.Pages.Base
{
    public abstract class BasePageModel : PageModel
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected BasePageModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected HttpClient CreateAuthorizedClient()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");

            var token = Request.Cookies["access_token"];
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
    }
}
