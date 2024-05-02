using System.Text.Json.Serialization;
using EventBus.Extensions;
using EventBus.RabbitMQ;
using Identity.Web.Data;
using Identity.Web.Events;
using Identity.Web.Models;

namespace Identity.Web.Extensions;

public static class Extensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<ApplicationDbContext>(Constants.POSTGRES_CONNECTION_NAME);
        
        builder.Services.AddDefaultIdentity<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddRazorPages();

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