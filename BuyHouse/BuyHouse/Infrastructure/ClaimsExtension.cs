namespace BuyHouse.Infrastructure
{
    using System.Security.Claims;

    using static Areas.Admin.AdminConstants;
    public static class ClaimsExtension
    {
        public static string GetId(this ClaimsPrincipal user) 
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal user) 
        {
            return user.IsInRole(AdministratolRoleName);
        }
    }
}
