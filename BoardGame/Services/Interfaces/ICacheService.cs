using StackExchange.Redis;

namespace BoardGame.Services.Interfaces
{
    public interface ICacheService
    {
        public Task<T?> GetDataAsync<T>(string key);

        public Task<RedisValue> HashGetAsync(string key, string subKey);

        public Task<HashEntry[]> HashGetAllAsync(string key);

        public Task HashSetAsync(string key, HashEntry[] entries, TimeSpan expiry = default);

        public Task<bool> RemoveDataAsync(string key);

        Task<bool> HashRemoveAsync(string key, string subkey);
    }
}
