using BoardGame.Providers.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;

namespace BoardGame.Providers
{
    public class RedisProvider : ICacheProvider
    {
        private Lazy<ConnectionMultiplexer> lazyConnection;

        private IServer RedisServer;

        private IDatabase _db;

        public RedisProvider(IConfig config)
        {
            var options = ConfigurationOptions.Parse(config.RedisConfig.ConnectionString);

            lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(options);
            });

            RedisServer = lazyConnection.Value.GetServer(options.EndPoints[0]);
            _db = lazyConnection.Value.GetDatabase();
        }

        public async Task<RedisValue> StringGetAsync(string key)
        {
            return await _db.StringGetAsync(key);
        }

        public async Task StringSetAsync(string key, RedisValue value)
        {
            await _db.StringSetAsync(key, value);
        }

        public async Task<bool> StringDeleteAsync(string key)
        {
            bool _isKeyExist = await _db.KeyExistsAsync(key);
            if (_isKeyExist == true)
            {
                return await _db.KeyDeleteAsync(key);
            }

            return false;
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

        public ICacheEntry CreateEntry(object key)
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(object key, out object? value)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
