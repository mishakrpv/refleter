using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Storage.Application.Dtos;
using Storage.DataAccess;
using Storage.Entities.Exceptions;
using Storage.Entities.Models.ScopeAggregate;

namespace Storage.Application.Commands.AddSecret;

public sealed class AddSecretRequestHandler(StorageContext context) : IRequestHandler<AddSecretRequest>
{
    private readonly StorageContext _context = context;
    
    public async Task Handle(AddSecretRequest request, CancellationToken cancellationToken)
    {
        var scope = await _context.Scopes.FirstOrDefaultAsync(s => s.Id == request.ScopeId);
        
        if (scope is null)
        {
            throw new EntityNotFoundException(typeof(Scope), request.ScopeId);
        }
        
        scope.AddSecret(request.Name, request.Value);
        _context.Update(scope);
        await _context.SaveChangesAsync();
    }
}