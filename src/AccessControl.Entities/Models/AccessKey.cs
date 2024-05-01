namespace AccessControl.Entities.Models;

public sealed class AccessKey
{
    public AccessKey(string userId, string key, string secretKey)
    {
        UserId = userId;
        Key = key;
        SecretKey = secretKey;
    }
    
    public string UserId { get; private set; }
    public string Key { get; private set; }
    public string SecretKey { get; private set; }
    public DateTime DateCreated { get; private set; } = DateTime.UtcNow;
}