using Identity.API.Data;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Extensions;

public static class Extensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<IdentityContext>(Constants.POSTGRES_CONNECTION_NAME);

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>();
        
        builder.AddIdentityServer();
    }
}