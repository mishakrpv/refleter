using Microsoft.AspNetCore.Http;

namespace Refleter.ServiceDefaults;

public static class MiddlewarePipelineExtensions
{
    public static bool IsApiRequest(this HttpContext context)
    {
        return context.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase)
               || context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
    }
}