using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Identity.API.Models;

namespace Identity.API.Extensions;

public static class IdentityServerExtensions
{
    public static void AddIdentityServer(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddIdentityServer()
            .AddInMemoryIdentityResources(Resources)
            .AddInMemoryApiResources(Apis)
            .AddInMemoryApiScopes(ApiScopes)
            .AddInMemoryClients(Clients(configuration))
            .AddAspNetIdentity<ApplicationUser>();
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
    
    private static IEnumerable<Client> Clients(IConfiguration configuration) =>
        new Client[]
        {
            new()
            {
                ClientId = "webapp",
                ClientName = "WebApp Client",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                ClientUri = $"{configuration["WebAppClient"]}",
                AllowedGrantTypes = GrantTypes.Code,
                AllowAccessTokensViaBrowser = false,
                RequireConsent = false,
                AllowOfflineAccess = true,
                AlwaysIncludeUserClaimsInIdToken = true,
                RequirePkce = false,
                RedirectUris = new List<string>
                {
                    $"{configuration["WebAppClient"]}/signin-oidc"
                },
                PostLogoutRedirectUris = new List<string>
                {
                    $"{configuration["WebAppClient"]}/signout-callback-oidc"
                },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "accesscontrol",
                    "storage"
                },
                
                AccessTokenLifetime = 60*60*2,
                IdentityTokenLifetime= 60*60*2
            },
            new()
            {
                ClientId = "storageswaggerui",
                ClientName = "Storage Swagger UI",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,
                RedirectUris = { $"{configuration["BasketApiClient"]}/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { $"{configuration["BasketApiClient"]}/swagger/" },
                
                AllowedScopes =
                {
                    "storage"
                }
            }
        };
}