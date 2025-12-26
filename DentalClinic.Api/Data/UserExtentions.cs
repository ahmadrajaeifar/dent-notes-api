using System.Security.Claims;

namespace DentalClinic.Api.Data
{
    public static class UserExtentions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            return int.Parse(
                user.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
