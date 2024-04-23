using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Storage.DataAccess;

namespace Storage.Application.Commands.DeleteScope;

public sealed class DeleteScopeRequestHandler(StorageContext context) : IRequestHandler<DeleteScopeRequest, Result>
{
    private readonly StorageContext _context = context;
    
    public async Task<Result> Handle(DeleteScopeRequest request, CancellationToken cancellationToken)
    {
        var scope = await _context.Scopes.FirstOrDefaultAsync(s => s.Id == request.Id);
        
        if (scope is null)
        {
            return Result.Fail($"Scope with id {request.Id} not found.");
        }
        
        _context.Scopes.Remove(scope);
        await _context.SaveChangesAsync();

        return Result.Ok();
    }
}