using EventBus.Abstractions;
using Storage.API.IntegrationEvents.Events;
using Storage.DataAccess;
using Storage.Entities.Models.ScopeAggregate;

namespace Storage.API.IntegrationEvents.EventHandling;

public sealed class UserCreatedIntegrationEventHandler(StorageContext context, ILogger<UserCreatedIntegrationEventHandler> logger)
    : IIntegrationEventHandler<UserCreatedIntegrationEvent>
{
    private readonly StorageContext _context = context;
    private readonly ILogger<UserCreatedIntegrationEventHandler> _logger = logger;
    
    public async Task Handle(UserCreatedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
        
        var scope = new Scope(@event.UserId, "default");
        _context.Scopes.Add(scope);
        await _context.SaveChangesAsync();
    }
}