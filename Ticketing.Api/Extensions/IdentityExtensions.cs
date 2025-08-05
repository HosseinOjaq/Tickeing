using System.Security.Claims;

namespace Ticketing.Api.Extensions;

public static class IdentityExtensions
{
    public static Guid GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        var nameIdentifier = httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.Parse(nameIdentifier!);
    }
}