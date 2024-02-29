using System.Security.Claims;

namespace Microservices_Net5.Helper
{
    public static class UserHelpers
    {
        /// <summary>
        /// Get user as provided token in API header 
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        public static string GetId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier) ?? principal.FindFirst(c => c.Type == "sub");
            if (userIdClaim != null && !string.IsNullOrEmpty(userIdClaim.Value))
            {
                return userIdClaim.Value;
            }

            return null;
        }

    }
}
