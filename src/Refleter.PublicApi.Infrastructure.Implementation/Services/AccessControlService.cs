using AccessControl.API.Grpc;
using Refleter.PublicApi.Infrastructure.Interfaces.Services;

using GrpcAccessControlClient = AccessControl.API.Grpc.AccessControl.AccessControlClient;

namespace Refleter.PublicApi.Infrastructure.Implementation.Services;

public sealed class AccessControlService(GrpcAccessControlClient client) : IAccessControlService
{
    private readonly GrpcAccessControlClient _client = client;
    
    public async Task<KeyValuePair<string, bool>> VerifyAccessKey(string secretKey)
    {
        var response = await _client.VerifyAccessKeyAsync(new VerificationRequest() { SecretKey = secretKey });

        return new KeyValuePair<string, bool>(response.UserId, response.Verified);
    }
}