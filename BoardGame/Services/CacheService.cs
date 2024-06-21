using BoardGame.Infrastractures;
using BoardGame.Services.Interfaces;
using StackExchange.Redis;

namespace BoardGame.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _db;

        public CacheService()
        {
            _db = ConnectionHelper.Connection.GetDatabase();
        }

        public async Task<RedisValue> StringGetAsync(string key)
        {
            return await _db.StringGetAsync(key);
        }

        public async Task StringSetAsync(string key, RedisValue value)
        {
            await _db.StringSetAsync(key, value);
        }

        public async Task<RedisValue> ListLeftPopAsync(string key)
        {
            return await _db.ListLeftPopAsync(key);
        }

        public async Task ListRightPushAsync(string key, RedisValue value)
        {
            await _db.ListRightPushAsync(key, value);
        }

        public async Task SetGetAsync(string key)
        {
            await _db.SetPopAsync(key);
        }

        public async Task<RedisValue> HashGetAsync(string key, string subKey)
        {
            return await _db.HashGetAsync(key, subKey);
        }

        public async Task<HashEntry[]> HashGetAllAsync(string key)
        {
            return await _db.HashGetAllAsync(key);
        }

        public async Task HashSetAsync(string key, HashEntry[] entries, TimeSpan expiry)
        {
            await _db.HashSetAsync(key, entries);
            await _db.KeyExpireAsync(key, expiry);
        }

        public async Task<bool> RemoveDataAsync(string key)
        {
            bool _isKeyExist = await _db.KeyExistsAsync(key);
            if (_isKeyExist == true)
            {
                return await _db.KeyDeleteAsync(key);
            }

            return false;
        }

        public async Task<bool> HashRemoveAsync(string key, string subkey)
        {
            bool _isKeyExist = await _db.HashExistsAsync(key, subkey);
            if (_isKeyExist == true)
            {
                return await _db.HashDeleteAsync(key, subkey);
            }

            return false;
        }
    }
}
