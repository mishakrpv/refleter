using Refleter.PublicApi.Infrastructure.Implementation.Services;
using Refleter.PublicApi.Infrastructure.Interfaces.Services;

namespace Refleter.PublicApi.API.Extensions;

public static class Extensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.AddRedisClient("redis");
        
        builder.Services.AddGrpcClient<AccessControl.API.Grpc.AccessControl.AccessControlClient>(o =>
            o.Address = new Uri("http://accesscontrolapi"));

        builder.Services.AddHttpClient<StorageService>(o =>
            o.BaseAddress = new Uri("http://storageapi"));

        builder.Services.AddSingleton<IAccessControlService, AccessControlService>();
    }
}