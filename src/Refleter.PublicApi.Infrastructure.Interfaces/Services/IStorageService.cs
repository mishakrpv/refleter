using Refleter.PublicApi.Model;

namespace Refleter.PublicApi.Infrastructure.Interfaces.Services;

public interface IStorageService
{
    Task<Scope?> GetScope(string userId, string name);
}