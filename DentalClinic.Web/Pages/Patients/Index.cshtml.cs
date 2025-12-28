using DentalClinic.Web.Pages.Base;
using Microsoft.AspNetCore.Authorization;

namespace DentalClinic.Web.Pages.Patients
{
    [Authorize(Policy = "RequireDentistRole")]
    public class IndexModel : BasePageModel
    {
        public IndexModel(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory)
        {
        }

        public async Task GetOn()
        {
            var client = CreateAuthorizedClient();
            var response = await client.GetAsync("api/patient");
        }
    }
}
