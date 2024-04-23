using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Storage.Application.Dtos;
using Storage.DataAccess;

namespace Storage.Application.Queries;

public sealed class Queries(StorageContext context, IMapper mapper) : IQueries
{
    private readonly StorageContext _context = context;
    private readonly IMapper _mapper = mapper;
    
    public async Task<Result<ScopeDTO>> GetScope(string id)
    {
        var scope = await _context.Scopes
            .Include(s => s.Icon)
            .Include(s => s.Secrets)
            .FirstOrDefaultAsync();

        if (scope is null)
        {
            return Result.Fail($"Scope with id {id} not found.");
        }

        return _mapper.Map<ScopeDTO>(scope);
    }
}