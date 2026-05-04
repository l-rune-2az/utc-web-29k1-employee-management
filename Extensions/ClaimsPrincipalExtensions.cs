using System.Security.Claims;

namespace EmployeeManagement.Extensions;

// Extension method cho ClaimsPrincipal — tránh lặp TryParse ở mọi controller
public static class ClaimsPrincipalExtensions
{
    // Trả về null nếu claim không tồn tại hoặc không parse được Guid
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
