using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Abstractions;

public interface IEventBusBuilder
{
    public IServiceCollection Services { get; }
}
