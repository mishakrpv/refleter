using AccessControl.Infrastructure;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.API.Grpc;

public sealed class AccessControlService(
    AccessControlContext context,
    ILogger<AccessControlService> logger) : AccessControl.AccessControlBase
{
    private readonly AccessControlContext _dbContext = context;
    private readonly ILogger<AccessControlService> _logger = logger;

    public override async Task<VerificationResponse> VerifyAccessKey(VerificationRequest request, ServerCallContext context)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Begin VerifyAccessKey call from method {Method} for key with id: {Id}", context.Method, request.Key);
        }
        
        VerificationResponse response = new() { Verified = false };

        var accessKey = await _dbContext.AccessKeys.FirstOrDefaultAsync(ak => ak.Key == request.Key && ak.SecretKey == request.SecretKey);

        if (accessKey is null || !accessKey.Active)
        {
            return response;
        }

        response.Verified = true;
        return response;
    }
}