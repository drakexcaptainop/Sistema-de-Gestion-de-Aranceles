using System.Security.Claims;

namespace SistemaDePagoDeAranceles.Application.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? GetUserId(this ClaimsPrincipal? user)
        {
            if (user == null) return null;
            var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(idClaim, out var id)) return id;
            return null;
        }
    }
}
