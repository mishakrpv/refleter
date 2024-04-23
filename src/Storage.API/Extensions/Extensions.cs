using Storage.DataAccess;

namespace Storage.API.Extensions;

public static class Extensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<StorageContext>(Constants.POSTGRES_CONNECTION_NAME);

        builder.Services.AddControllers();
    }
    
    public static void AddHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString(
                Constants.POSTGRES_CONNECTION_NAME) ?? throw new InvalidOperationException($"ConnectionStrings missing value for {Constants.POSTGRES_CONNECTION_NAME}"),
                name: "PostgresCheck");
    }
}