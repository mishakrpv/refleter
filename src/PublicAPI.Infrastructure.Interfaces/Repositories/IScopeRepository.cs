using PublicAPI.Model;

namespace PublicAPI.Infrastructure.Interfaces.Repositories;

public interface IScopeRepository
{
    Task<Scope> CreateScopeAsync(Scope scope);
    
    Task<Scope?> GetScopeAsync(string id);
    
    Task<Scope?> UpdateScopeAsync(Scope scope);
    
    Task<bool> DeleteScopeAsync(string id);
}