using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Refleter.ServiceDefaults;

public static class OpenApiExtensions
{
    public static IHostApplicationBuilder AddDefaultOpenApi(this IHostApplicationBuilder builder)
    {
        return builder;
    }
    
    public static IApplicationBuilder UseDefaultOpenApi(this WebApplication app)
    {
        return app;
    }
}