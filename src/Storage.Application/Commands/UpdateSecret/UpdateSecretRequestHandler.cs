using MediatR;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccess;
using Storage.Entities.Exceptions;
using Storage.Entities.Models.ScopeAggregate;

namespace Storage.Application.Commands.UpdateSecret;

public sealed class UpdateSecretRequestHandler(StorageContext context) : IRequestHandler<UpdateSecretRequest>
{
    private readonly StorageContext _context = context;
    
    public async Task Handle(UpdateSecretRequest request, CancellationToken cancellationToken)
    {
        var scope = await _context.Scopes.Include(scope => scope.Secrets).FirstOrDefaultAsync(s => s.Id == request.ScopeId);
        
        if (scope is null)
        {
            throw new EntityNotFoundException(typeof(Scope), request.ScopeId);
        }

        var secret = scope.Secrets.FirstOrDefault(s => s.Id == request.Id) ?? throw new EntityNotFoundException(typeof(Secret), request.Id);
        secret.UpdateValue(request.Value);
        _context.Update(scope);
        await _context.SaveChangesAsync();
    }
}