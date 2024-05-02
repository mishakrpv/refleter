namespace AccessControl.Entities.Models;

public sealed class AccessKey
{
    public AccessKey(string userId, string id, string secretKey)
    {
        UserId = userId;
        Id = id;
        SecretKey = secretKey;
    }
    
    public string UserId { get; private set; }
    public string Id { get; private set; }
    public string SecretKey { get; private set; }
    public bool Active { get; private set; }
    public DateTime DateCreated { get; private set; } = DateTime.UtcNow;

    public void SetActivityStatus(bool active)
    {
        Active = active;
    }
}