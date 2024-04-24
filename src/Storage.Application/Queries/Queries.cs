using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Storage.Application.Dtos;
using Storage.DataAccess;
using Storage.Entities.Exceptions;
using Storage.Entities.Models.ScopeAggregate;

namespace Storage.Application.Queries;

public sealed class Queries(StorageContext context, IMapper mapper) : IQueries
{
    private readonly StorageContext _context = context;
    private readonly IMapper _mapper = mapper;
    
    public async Task<ScopeDTO> GetScope(string id)
    {
        var scope = await _context.Scopes
            .Include(s => s.Icon)
            .Include(s => s.Secrets)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (scope is null)
        {
            throw new EntityNotFoundException(typeof(Scope), id);
        }

        return _mapper.Map<ScopeDTO>(scope);
    }

    public async Task<IEnumerable<ScopeDTO>> GetScopesByUserId(string userId)
    {
        var scopes = await _context.Scopes
            .Where(s => s.UserId == userId)
            .Include(s => s.Icon)
            .Include(s => s.Secrets)
            .Select(s => _mapper.Map<ScopeDTO>(s))
            .ToListAsync();

        return scopes;
    }
}