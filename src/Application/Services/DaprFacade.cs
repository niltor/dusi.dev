using Dapr.Client;

namespace Application.Services;
/// <summary>
/// 提供dapr相关静态方法
/// </summary>
public class DaprFacade
{
    public static DaprClient Dapr = new DaprClientBuilder().Build();
    public static string DEFAULT_STATE = "statestore";
    public static string DEFAULT_PUBSUB = "pubsub";

    /// <summary>
    /// 保存状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="seconds">过期时间秒</param>
    /// <returns></returns>
    public static async Task SaveStateAsync<T>(string key, T value, int seconds)
    {
        var metadata = new Dictionary<string, string>
        {
            { "ttlInSeconds", seconds.ToString() }
        };
        await Dapr.SaveStateAsync(DEFAULT_STATE, key, value, metadata: metadata);
    }
    public static async Task SaveStateAsync<T>(string store, string key, T value, int seconds)
    {
        var metadata = new Dictionary<string, string>
        {
            { "ttlInSeconds", seconds.ToString() }
        };
        await Dapr.SaveStateAsync(store, key, value, metadata: metadata);
    }

    /// <summary>
    /// 获取状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public static async Task<T> GetStateAsync<T>(string key)
    {
        return await Dapr.GetStateAsync<T>(DEFAULT_STATE, key);
    }
    public static async Task<T> GetStateAsync<T>(string stroe, string key)
    {
        return await Dapr.GetStateAsync<T>(stroe, key);
    }


    public static async Task PublishAsync<T>(string topic, T data)
    {
        await PublishAsync<T>(DEFAULT_PUBSUB, topic, data);
    }


    public static async Task PublishAsync<T>(string name, string topic, T data)
    {
        await Dapr.PublishEventAsync<T>(name, topic, data);
    }
}
