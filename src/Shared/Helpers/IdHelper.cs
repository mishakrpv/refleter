internal static class IdHelper
{
    public static string NewStringId()
    {
        return Guid.NewGuid().ToString().Replace("-", string.Empty);
    }
}