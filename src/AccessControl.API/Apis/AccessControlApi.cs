using AccessControl.API.Application.Commands.CreateAccessKey;
using AccessControl.API.DI;
using AccessControl.API.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.API.Apis;

public static class AccessControlApi
{
    public static IEndpointRouteBuilder MapAccessControlApi(this IEndpointRouteBuilder app)
    {
        app.MapPost("/key", CreateAccessKey);
        
        return app;
    }

    public static async Task<Ok<AccessKeyOneTimeDTO>> CreateAccessKey(
        [AsParameters] AccessControlServices services,
        [FromBody] CreateAccessKeyRequest request)
    {
        return TypedResults.Ok(await services.Mediator.Send(request));
    }
}