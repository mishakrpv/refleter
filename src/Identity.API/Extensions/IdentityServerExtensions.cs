using Duende.IdentityServer.Models;

namespace Identity.API.Extensions;

public static class IdentityServerExtensions
{
    public static void AddIdentityServer(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentityServer()
            .AddInMemoryIdentityResources(Resources)
            .AddInMemoryApiResources(Apis)
            .AddInMemoryApiScopes(ApiScopes)
            .AddInMemoryClients(Clients);
    }
    
    private static IEnumerable<IdentityResource> Resources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    
    private static IEnumerable<ApiResource> Apis => 
        new List<ApiResource>
        {
            new(name: "storage", displayName: "Storage API")
        };
    
    private static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new(name: "storage", displayName: "Storage API") 
        };
    
    private static IEnumerable<Client> Clients =>
        new Client[]
        {
            new()
            {
                ClientId = "webapp",
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedScopes = { "storage" }
            }
        };
}