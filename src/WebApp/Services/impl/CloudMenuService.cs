namespace WebApp.Services.Impl;

public class CloudMenuService(StorageService storageService)
{
    private readonly StorageService _storageService = storageService;
    
    public readonly State State = new();

    public async Task<ScopeRecord[]> GetScopesAsync(string cloudId)
    {
        return State.Scopes ?? (State.Scopes = await _storageService.GetScopesByCloudIdAsync(cloudId));
    }

    public void SetActiveMenuItem(string url)
    {
        if (State.Scopes == null) return;
        var startIndex = url.LastIndexOf('/') + 1;
        var itemId = url[startIndex..];
        State.MenuHaveActiveItem = false;
        foreach (var item in State.Scopes)
        {
            if (item.Id != itemId) continue;
            item.IsActive = true;
            State.MenuHaveActiveItem = true;
        }
    }
}

public class State
{
    public ScopeRecord[]? Scopes { get; set; }
    public bool MenuExpanded { get; set; }
    public bool MenuHaveActiveItem { get; set; }
}