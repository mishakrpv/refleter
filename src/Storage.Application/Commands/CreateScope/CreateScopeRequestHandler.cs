using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Storage.Application.Dtos;
using Storage.DataAccess;
using Storage.Entities.Models.ScopeAggregate;

namespace Storage.Application.Commands.CreateScope;

public sealed class CreateScopeRequestHandler(StorageContext context, IMapper mapper) : IRequestHandler<CreateScopeRequest, Result<ScopeDTO>>
{
    private readonly StorageContext _context = context;
    private readonly IMapper _mapper = mapper;
    
    public async Task<Result<ScopeDTO>> Handle(CreateScopeRequest request, CancellationToken cancellationToken)
    {
        var scopeEntry = await _context.AddAsync(new Scope(request.UserId, request.Name));

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return Result.Fail(new Error($"A scope named {request.Name} already exists.").CausedBy(ex));
        }

        return _mapper.Map<ScopeDTO>(scopeEntry.Entity);
    }
}