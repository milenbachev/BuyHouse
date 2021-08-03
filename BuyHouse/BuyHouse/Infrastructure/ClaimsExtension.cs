namespace BuyHouse.Infrastructure
{
    using System.Security.Claims;

    public static class ClaimsExtension
    {
        public static string GetId(this ClaimsPrincipal user) 
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
