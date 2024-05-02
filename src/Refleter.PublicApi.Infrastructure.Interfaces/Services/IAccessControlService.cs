namespace Refleter.PublicApi.Infrastructure.Interfaces.Services;

public interface IAccessControlService
{
    Task<KeyValuePair<string, bool>> VerifyAccessKey(string secretKey);
}