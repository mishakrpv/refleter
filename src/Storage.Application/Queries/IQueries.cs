using Storage.Application.Dtos;

namespace Storage.Application.Queries;

public interface IQueries
{
    Task<ScopeDTO> GetScope(string id);

    Task<IEnumerable<ScopeDTO>> GetScopesByUserId(string userId);

    Task<ScopeDTO> GetScopeByUserIdAndName(string userId, string name);
}