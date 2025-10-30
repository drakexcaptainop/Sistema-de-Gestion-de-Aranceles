using System.Security.Claims;
using Common.Infrastructure.Logging;

namespace Common.Infrastructure.Logger;

public static class AuditHelper
{
    public static void LogUserAction(ClaimsPrincipal user, string action, string entity, string details)
    {
        var logger = new AuditLogger();
        string userName = user?.Identity?.Name ?? "Unknown";
        string userId = user?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "N/A";
        string role = user?.Claims.FirstOrDefault(c => c.Type == "role")?.Value ?? "N/A";
        logger.Log(userName, userId, role, action, entity, details);
    }
}
