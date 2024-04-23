using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Storage.Application.Dtos;
using Storage.DataAccess;

namespace Storage.Application.Commands.UpdateScope;

public sealed class UpdateScopeRequestHandler(StorageContext context, IMapper mapper) : IRequestHandler<UpdateScopeRequest, Result<ScopeDTO>>
{
    private readonly StorageContext _context = context;
    private readonly IMapper _mapper = mapper;
    
    public async Task<Result<ScopeDTO>> Handle(UpdateScopeRequest request, CancellationToken cancellationToken)
    {
        var scope = await _context.Scopes.FirstOrDefaultAsync(s => s.Id == request.Id);
        
        if (scope is null)
        {
            return Result.Fail($"Scope with id {request.Id} not found.");
        }
        
        scope.UpdateName(request.Name);
        _context.Update(scope);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<ScopeDTO>(scope);
    }
}