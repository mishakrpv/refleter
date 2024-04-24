using MediatR;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccess;
using Storage.Entities.Exceptions;
using Storage.Entities.Models.ScopeAggregate;

namespace Storage.Application.Commands.DeleteScope;

public sealed class DeleteScopeRequestHandler(StorageContext context) : IRequestHandler<DeleteScopeRequest>
{
    private readonly StorageContext _context = context;
    
    public async Task Handle(DeleteScopeRequest request, CancellationToken cancellationToken)
    {
        var scope = await _context.Scopes.FirstOrDefaultAsync(s => s.Id == request.Id);
        
        if (scope is null)
        {
            throw new EntityNotFoundException(typeof(Scope), request.Id);
        }
        
        _context.Scopes.Remove(scope);
        await _context.SaveChangesAsync();
    }
}