using EventBus.Abstractions;
using Storage.API.IntegrationEvents.Events;
using Storage.DataAccess;
using Storage.Entities.Models.ScopeAggregate;

namespace Storage.API.IntegrationEvents.EventHandling;

public sealed class ScopeCreatedIntegrationEventHandler(StorageContext context, ILogger<ScopeCreatedIntegrationEventHandler> logger)
    : IIntegrationEventHandler<ScopeCreatedIntegrationEvent>
{
    private readonly StorageContext _context = context;
    private readonly ILogger<ScopeCreatedIntegrationEventHandler> _logger = logger;
    
    public async Task Handle(ScopeCreatedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
        
        var scope = new Scope(@event.UserId, @event.ScopeName);
        await _context.AddAsync(scope);
        await _context.SaveChangesAsync();
    }
}