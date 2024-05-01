using MediatR;

namespace AccessControl.API.Application.Commands.CreateAccessKey;

public sealed class CreateAccessKeyRequestHandler : IRequestHandler<CreateAccessKeyRequest>
{
    public async Task Handle(CreateAccessKeyRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}