﻿using Microsoft.Extensions.Caching.Distributed;
namespace Application.Services;

/// <summary>
/// 简单封装对象的存储和获取
/// </summary>
public class CacheService
{
    private readonly IDistributedCache _cache;
    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    /// <summary>
    /// 保存到缓存
    /// </summary>
    /// <param name="data">值</param>
    /// <param name="key">键</param>
    /// <param name="sliding">相对过期时间</param>
    /// <param name="expiration">绝对过期时间</param>
    /// <returns></returns>
    public async Task SetValueAsync(string key, object data, int sliding, int expiration)
    {
        var bytes = JsonSerializer.SerializeToUtf8Bytes(data);
        await _cache.SetAsync(key, bytes, new DistributedCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromSeconds(sliding),
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expiration)
        });
    }
    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public T? GetValue<T>(string key)
    {
        var bytes = _cache.Get(key);
        if (bytes == null || bytes.Length < 1)
        {
            return default;
        }
        var readOnlySpan = new ReadOnlySpan<byte>(bytes);
        return JsonSerializer.Deserialize<T>(readOnlySpan);
    }
}