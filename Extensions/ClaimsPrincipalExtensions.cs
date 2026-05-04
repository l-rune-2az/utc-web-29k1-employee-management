using System.Security.Claims;

namespace EmployeeManagement.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid? GetEmployeeId(this ClaimsPrincipal user)
    {
        string? value = user.FindFirst("EmployeeId")?.Value;
        return Guid.TryParse(value, out Guid id) ? id : null;
    }

    public static string GetRole(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
    }
}
