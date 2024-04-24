using AutoMapper;
using MediatR;
using Storage.Application.Dtos;
using Storage.DataAccess;
using Storage.Entities.Models.ScopeAggregate;

namespace Storage.Application.Commands.CreateScope;

public sealed class CreateScopeRequestHandler(StorageContext context, IMapper mapper) : IRequestHandler<CreateScopeRequest, ScopeDTO>
{
    private readonly StorageContext _context = context;
    private readonly IMapper _mapper = mapper;
    
    public async Task<ScopeDTO> Handle(CreateScopeRequest request, CancellationToken cancellationToken)
    {
        var scopeEntry = await _context.AddAsync(new Scope(request.UserId, request.Name));
        await _context.SaveChangesAsync();

        return _mapper.Map<ScopeDTO>(scopeEntry.Entity);
    }
}