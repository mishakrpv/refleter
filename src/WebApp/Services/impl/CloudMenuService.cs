namespace WebApp.Services.Impl;

public class CloudMenuService(StorageService storageService)
{
    private readonly StorageService _storageService = storageService;
    
    public readonly State State = new();

    public async Task<MenuItem[]?> GetMenuItemsAsync(string cloudId)
    {
        return State.Items ?? (State.Items = (await _storageService.GetScopesByCloudIdAsync(cloudId))
            .Select(s => new MenuItem(s.Id, s.Name, s.IconColorHexCode, s.DateCreated, s.Secrets)).ToArray());
    }

    public void SetActiveMenuItem(string url)
    {
        if (State.Items == null) return;
        var startIndex = url.LastIndexOf('/') + 1;
        var itemId = url[startIndex..];
        State.MenuHasActiveItem = false;
        foreach (var item in State.Items)
        {
            if (item.Id != itemId) continue;
            item.IsActive = true;
            State.MenuHasActiveItem = true;
        }
    }
}

public class State
{
    public MenuItem[]? Items { get; set; }
    public bool MenuExpanded { get; set; }
    public bool MenuHasActiveItem { get; set; }
}

public record MenuItem(string Id, string Name, string IconColorHexCode, DateTime DateCreated, SecretRecord[] Secrets) : ScopeRecord(Id, Name, IconColorHexCode, DateCreated, Secrets)
{
    public bool IsActive { get; set; }
}