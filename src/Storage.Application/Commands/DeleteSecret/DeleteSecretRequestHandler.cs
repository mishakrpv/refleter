using MediatR;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccess;
using Storage.Entities.Exceptions;
using Storage.Entities.Models.ScopeAggregate;

namespace Storage.Application.Commands.DeleteSecret;

public sealed class DeleteSecretRequestHandler(StorageContext context) : IRequestHandler<DeleteSecretRequest>
{
    private readonly StorageContext _context = context;
    
    public async Task Handle(DeleteSecretRequest request, CancellationToken cancellationToken)
    {
        var scope = await _context.Scopes.FirstOrDefaultAsync(s => s.Id == request.ScopeId);
        
        if (scope is null)
        {
            throw new EntityNotFoundException(typeof(Scope), request.ScopeId);
        }

        if (!scope.RemoveSecret(request.Id)) throw new EntityNotFoundException(typeof(Secret), request.Id);
        _context.Update(scope);
        await _context.SaveChangesAsync();
        
    }
}