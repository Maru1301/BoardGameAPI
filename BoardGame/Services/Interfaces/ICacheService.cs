namespace BoardGame.Services.Interfaces;

public interface ICacheService
{
    T? Get<T>(object key) where T : class, new();

    void Set<T>(string key, T value, TimeSpan timeSpan) where T : class, new();

    void Remove<T>(object key);
}

public enum CacheDataStatus
{
    /// <summary>
    /// 
    /// </summary>
    None,
    /// <summary>
    /// 
    /// </summary>
    OK,
    /// <summary>
    /// 
    /// </summary>
    Expired
}
