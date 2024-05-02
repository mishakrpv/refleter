using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Refleter.PublicApi.API.DI;
using Refleter.PublicApi.Model;

namespace Refleter.PublicApi.API.Apis;

public static class PublicApi
{
    public static RouteHandlerBuilder MapPublicApi(this IEndpointRouteBuilder app)
        => app.MapGet("/{scopeName}", GetScopeSecrets);

    public static async Task<Results<Ok<List<Secret>>, NotFound, UnauthorizedHttpResult>> GetScopeSecrets(
        [AsParameters] PublicApiServices services,
        [FromHeader(Name = "Authorization")] string credential,
        [FromRoute] string scopeName)
    {
        var result = await services.AccessControl.VerifyAccessKey(credential);

        if (!result.Value)
        {
            return TypedResults.Unauthorized();
        }

        var scope = await services.Repository.GetScopeAsync($"{result.Key}:{scopeName}");
        if (scope is null)
        {
            scope = await services.Storage.GetScope(result.Key, scopeName);
            if (scope is null)
            {
                return TypedResults.NotFound();
            }

            await services.Repository.CreateScopeAsync(scope);
        }

        return TypedResults.Ok(scope.Secrets);
    }
}