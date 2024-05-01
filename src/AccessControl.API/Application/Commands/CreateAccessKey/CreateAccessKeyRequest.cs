using AccessControl.API.Dtos;
using MediatR;

namespace AccessControl.API.Application.Commands.CreateAccessKey;

public sealed record CreateAccessKeyRequest(string UserId) : IRequest<AccessKeyOneTimeDTO>;