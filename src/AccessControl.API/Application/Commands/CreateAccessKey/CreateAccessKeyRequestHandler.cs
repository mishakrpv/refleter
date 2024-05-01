using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;
using AccessControl.API.Dtos;
using AccessControl.Entities.Models;
using AccessControl.Infrastructure;
using MediatR;

namespace AccessControl.API.Application.Commands.CreateAccessKey;

public sealed class CreateAccessKeyRequestHandler(AccessControlContext context) : IRequestHandler<CreateAccessKeyRequest, AccessKeyOneTimeDTO>
{
    private readonly AccessControlContext _context = context;
    
    public async Task<AccessKeyOneTimeDTO> Handle(CreateAccessKeyRequest request, CancellationToken cancellationToken)
    {
        var accessKeyEntry = await _context.AccessKeys.AddAsync(new AccessKey(request.UserId, GetSecretKey(24), GetSecretKey(32)));
        await _context.SaveChangesAsync();
        
        return MapKeyToDTO(accessKeyEntry.Entity);
    }
    
    private static string GetSecretKey(int volume)
    {
        var key = new byte[volume];
        
        RandomNumberGenerator.Create().GetBytes(key);
        return Convert.ToBase64String(key).TrimEnd('=').Replace("/", "").Replace("+", "");
    }

    public static AccessKeyOneTimeDTO MapKeyToDTO(AccessKey key)
    {
        return new AccessKeyOneTimeDTO
        {
            Key = key.Key,
            SecretKey = key.SecretKey
        };
    }
}