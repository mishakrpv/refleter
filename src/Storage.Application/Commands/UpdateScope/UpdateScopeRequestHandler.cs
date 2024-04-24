using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Storage.Application.Dtos;
using Storage.DataAccess;
using Storage.Entities.Exceptions;
using Storage.Entities.Models.ScopeAggregate;

namespace Storage.Application.Commands.UpdateScope;

public sealed class UpdateScopeRequestHandler(StorageContext context, IMapper mapper) : IRequestHandler<UpdateScopeRequest, ScopeDTO>
{
    private readonly StorageContext _context = context;
    private readonly IMapper _mapper = mapper;
    
    public async Task<ScopeDTO> Handle(UpdateScopeRequest request, CancellationToken cancellationToken)
    {
        var scope = await _context.Scopes.FirstOrDefaultAsync(s => s.Id == request.Id);
        
        if (scope is null)
        {
            throw new EntityNotFoundException(typeof(Scope), request.Id);
        }
        
        scope.UpdateName(request.Name);
        _context.Update(scope);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<ScopeDTO>(scope);
    }
}