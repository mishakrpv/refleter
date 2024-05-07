using System.Text.Json.Serialization;
using EventBus.Extensions;
using EventBus.RabbitMQ;
using Identity.Web.Data;
using Identity.Web.Events;
using Identity.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Web.Extensions;

public static class Extensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<ApplicationDbContext>(Constants.POSTGRES_CONNECTION_NAME);
        
        builder.Services.AddDefaultIdentity<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddRazorPages();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.RequireUniqueEmail = true;
        });

        builder.AddRabbitMqEventBus(Constants.EVENT_BUS_CONNECTION_NAME).ConfigureJsonOptions(options => options.TypeInfoResolverChain.Add(IntegrationEventContext.Default));
        
        builder.AddAuthentication();
    }

    public static void AddHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString(
                Constants.POSTGRES_CONNECTION_NAME) ??
                    throw new InvalidOperationException(
                        $"ConnectionStrings missing value for {Constants.POSTGRES_CONNECTION_NAME}"),
                name: "PostgresCheck");
    }
}

[JsonSerializable(typeof(UserCreatedIntegrationEvent))]
partial class IntegrationEventContext : JsonSerializerContext
{
    
}