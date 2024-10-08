﻿using System.Text.Json.Serialization;
using EventBus.Extensions;
using EventBus.RabbitMQ;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refleter.ServiceDefaults;
using Storage.API.IntegrationEvents.EventHandling;
using Storage.API.IntegrationEvents.Events;
using Storage.Application.Commands.CreateScope;
using Storage.Application.Mapping;
using Storage.Application.Queries;
using Storage.DataAccess;
using Storage.Entities.Exceptions;

namespace Storage.API.Extensions;

public static class Extensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.AddDefaultAuthentication();
        
        builder.AddNpgsqlDbContext<StorageContext>(Constants.POSTGRES_CONNECTION_NAME);
        
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CreateScopeRequest).Assembly));
        
        builder.Services.AddScoped<IQueries, Queries>();

        builder.AddRabbitMqEventBus(Constants.EVENT_BUS_CONNECTION_NAME)
            .AddSubscription<UserCreatedIntegrationEvent, UserCreatedIntegrationEventHandler>()
            .AddSubscription<ScopeCreatedIntegrationEvent, ScopeCreatedIntegrationEventHandler>()
            .ConfigureJsonOptions(options => options.TypeInfoResolverChain.Add(IntegrationEventContext.Default));

        builder.Services.AddControllers();
        builder.AddProblemDetails();
    }

    private static void AddProblemDetails(this WebApplicationBuilder builder)
    {
        builder.Services.AddProblemDetails(options =>
        {
            options.IncludeExceptionDetails = (context, ex) => builder.Environment.IsDevelopment() || builder.Environment.IsStaging();
            
            options.Map<DbUpdateException>((context, ex) => new ProblemDetails
            {
                Title = "Entity cannot be saved.",
                Detail = $"A {ex.Entries.First().Entity.GetType().Name} with this name already exists.",
                Status = StatusCodes.Status409Conflict,
                Type = $"https://httpstatuses.com/{StatusCodes.Status409Conflict}",
                Instance = context.Request.Path
            });
            
            options.Map<EntityNotFoundException>((context, ex) => new ProblemDetails
            {
                Title = "Entity cannot be found.",
                Detail = ex.Message,
                Status = StatusCodes.Status404NotFound,
                Type = $"https://httpstatuses.com/{StatusCodes.Status404NotFound}",
                Instance = context.Request.Path
            });
        });
    }
    
    public static void AddHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString(
                Constants.POSTGRES_CONNECTION_NAME) ?? throw new InvalidOperationException($"ConnectionStrings missing value for {Constants.POSTGRES_CONNECTION_NAME}"),
                name: "PostgresCheck");
    }
}

[JsonSerializable(typeof(UserCreatedIntegrationEvent))]
partial class IntegrationEventContext : JsonSerializerContext
{

}