using Refleter.PublicApi.Infrastructure.Interfaces.Repositories;
using Refleter.PublicApi.Infrastructure.Interfaces.Services;

namespace Refleter.PublicApi.API.DI;

public sealed class PublicApiServices(
    IAccessControlService accessControl,
    IScopeRepository repository,
    IStorageService storage,
    ILogger<PublicApiServices> logger)
{
    public IAccessControlService AccessControl { get; } = accessControl;
    public IScopeRepository Repository { get; } = repository;
    public IStorageService Storage { get; } = storage;
    public ILogger<PublicApiServices> Logger { get; } = logger;
}