using BoardGame.Models.EFModels;
using StackExchange.Redis;

namespace BoardGame.Services.Interfaces
{
    public interface ICacheService
    {
        public Task<RedisValue> StringGetAsync(string key);

        public Task StringSetAsync(string key, RedisValue value);

        public Task<RedisValue> ListLeftPopAsync(string key);

        public Task ListRightPushAsync(string key, RedisValue value);

        public Task<RedisValue> HashGetAsync(string key, string subKey);

        public Task<HashEntry[]> HashGetAllAsync(string key);

        public Task HashSetAsync(string key, HashEntry[] entries) => HashSetAsync(key, entries, TimeSpan.FromMinutes(5));

        public Task HashSetAsync(string key, HashEntry[] entries, TimeSpan expiry);

        public Task<bool> RemoveDataAsync(string key);

        Task<bool> HashRemoveAsync(string key, string subkey);
    }
}
