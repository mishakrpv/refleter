using FluentResults;
using Storage.Application.Dtos;

namespace Storage.Application.Queries;

public interface IQueries
{
    Task<Result<ScopeDTO>> GetScope(string id);
}