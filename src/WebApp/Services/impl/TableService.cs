namespace WebApp.Services.Impl;

public class TableService(StorageService storageService)
{
    private readonly StorageService _storageService = storageService;

    private List<TableItem>? _items;

    public async Task<List<TableItem>> GetItemsAsync(string cloudId)
    {
        return _items ??= (await _storageService.GetScopesByCloudIdAsync(cloudId))
            .Select(s => new TableItem(s.Id, s.Name, s.IconColorHexCode, s.DateCreated, s.Secrets)).ToList();
    }

    public async Task UpdateAsync(string cloudId)
    {
        _items = (await _storageService.GetScopesByCloudIdAsync(cloudId))
            .Select(s => new TableItem(s.Id, s.Name, s.IconColorHexCode, s.DateCreated, s.Secrets)).ToList();
    }

    public void ClearItems()
    {
        _items = null;
    }
}

public record TableItem(string Id, string Name, string IconColorHexCode, DateTime DateCreated, SecretRecord[] Secrets) : ScopeRecord(Id, Name, IconColorHexCode, DateCreated, Secrets);