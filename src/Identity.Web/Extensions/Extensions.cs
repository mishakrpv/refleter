using Identity.Web.Data;
using Identity.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Web.Extensions;

public static class Extensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Logging.AddConsole();
        
        builder.AddNpgsqlDbContext<ApplicationDbContext>(Constants.POSTGRES_CONNECTION_NAME);
        
        builder.Services.AddDefaultIdentity<ApplicationsUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddRazorPages();
        
        builder.AddAuthentication();
    }

    public static void AddHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString(
                Constants.POSTGRES_CONNECTION_NAME) ?? throw new InvalidOperationException($"ConnectionStrings missing value for {Constants.POSTGRES_CONNECTION_NAME}"),
                name: "PostgresCheck");
    }
}