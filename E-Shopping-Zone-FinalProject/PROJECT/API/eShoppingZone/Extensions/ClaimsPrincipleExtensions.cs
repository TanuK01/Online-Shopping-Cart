using System.Security.Claims;

namespace eShoppingZoneAPI.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string RetrieveEmailFromPrinciple(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}
