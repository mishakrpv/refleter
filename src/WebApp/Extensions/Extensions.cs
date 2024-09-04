using System.Text.Json.Serialization;
using EventBus.Extensions;
using EventBus.RabbitMQ;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.IdentityModel.JsonWebTokens;
using Refleter.ServiceDefaults;
using WebApp.Events;
using WebApp.Services.Impl;

namespace WebApp.Extensions;

public static class Extensions
{
    public static void AddServices(this IHostApplicationBuilder builder)
    {
        builder.AddAuthenticationServices();
        
        builder.Services.AddHttpForwarderWithServiceDiscovery();
        
        builder.Services.AddHttpClient<StorageService>(hc => hc.BaseAddress = new Uri("http://storageapi"))
            .AddApiVersion(1.0)
            .AddAuthToken();
        
        builder.AddRabbitMqEventBus("eventbus").ConfigureJsonOptions(options => options.TypeInfoResolverChain.Add(IntegrationEventContext.Default));

        builder.Services.AddScoped<CloudMenuService>();
        builder.Services.AddScoped<TableService>();
    }

    private static void AddAuthenticationServices(this IHostApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;

        JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        var identityUrl = configuration.GetRequiredValue("IdentityUrl");
        var callBackUrl = configuration.GetRequiredValue("CallBackUrl");
        var sessionCookieLifetime = configuration.GetValue("SessionCookieLifetimeMinutes", 60);

        // Add Authentication services
        services.AddAuthorization();
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(options => options.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime))
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = identityUrl;
                options.SignedOutRedirectUri = callBackUrl;
                options.ClientId = "webapp";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.RequireHttpsMetadata = false;
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("storage");
            });

        // Blazor auth services
        services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
        services.AddCascadingAuthenticationState();
    }
}

[JsonSerializable(typeof(ScopeCreatedIntegrationEvent))]
partial class IntegrationEventContext : JsonSerializerContext
{
    
}