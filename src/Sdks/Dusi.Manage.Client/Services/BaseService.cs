using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Share.Models.AuthDtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dusi.Manage.Client.Services;
public class BaseService
{
    protected HttpClient Http { get; set; }
    protected JsonSerializerOptions JsonSerializerOptions { get; set; }
    protected ErrorResult? ErrorMsg { get; set; } = null;

    public BaseService(HttpClient httpClient)
    {
        Http = httpClient;
        JsonSerializerOptions = new JsonSerializerOptions()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
    }



    /// <summary>
    /// json post 封装
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="route"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<TResult?> PostJsonAsync<T, TResult>(string route, T data)
    {
        return await SendJsonAsync<T, TResult>(HttpMethod.Post, route, data);
    }

    /// <summary>
    /// Put
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="route"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<TResult?> PutJsonAsync<T, TResult>(string route, T data)
    {
        return await SendJsonAsync<T, TResult>(HttpMethod.Put, route, data);
    }


    public async Task<TResult?> GetJsonAsync<TResult>(string route, Dictionary<string, string?>? dic = null)
    {
        return await SendJsonAsync<TResult>(HttpMethod.Get, route, dic);
    }

    public async Task<TResult?> DeleteJsonAsync<TResult>(string route, Dictionary<string, string?>? dic = null)
    {
        return await SendJsonAsync<TResult>(HttpMethod.Delete, route, dic);
    }

    public static string ToUrlParameters(Dictionary<string, string?> dic)
    {
        return string.Join("&", dic.Where(d => d.Value != null)
            .Select(d => string.Format("{0}={1}", d.Key, d.Value))
            );
    }

    public async Task<TResult?> SendJsonAsync<T, TResult>(HttpMethod method, string route, T data)
    {
        HttpResponseMessage? res = null;
        if (method == HttpMethod.Post)
        {
            res = await Http.PostAsJsonAsync(route, data, JsonSerializerOptions);
        }
        else if (method == HttpMethod.Put)
        {
            res = await Http.PutAsJsonAsync(route, data, JsonSerializerOptions);
        }
        if (res != null && res.IsSuccessStatusCode)
        {
            return await res.Content.ReadFromJsonAsync<TResult>();
        }
        else
        {
            ErrorMsg = await res!.Content.ReadFromJsonAsync<ErrorResult>();
            return default;
        }
    }

    public async Task<TResult?> SendJsonAsync<TResult>(HttpMethod method, string route, Dictionary<string, string?>? dic = null)
    {
        if (dic != null)
        {
            route = route + "?" + ToUrlParameters(dic);
        }
        HttpResponseMessage? res = null;
        if (method == HttpMethod.Get)
        {
            res = await Http.GetAsync(route);

        }
        else if (method == HttpMethod.Delete)
        {
            res = await Http.DeleteAsync(route);

        }
        if (res != null && res.IsSuccessStatusCode)
        {
            return await res.Content.ReadFromJsonAsync<TResult>();
        }
        else
        {
            ErrorMsg = await res!.Content.ReadFromJsonAsync<ErrorResult>();
            return default;
        }
    }
}
