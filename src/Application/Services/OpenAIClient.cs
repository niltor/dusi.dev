using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace Application.Services;
/// <summary>
/// openai 请求服务
/// </summary>
public class OpenAIClient
{
    private readonly HttpClient Client;
    private readonly IConfiguration _configuration;

    ILogger<OpenAIClient> _logger;

    public OpenAIClient(HttpClient client, IConfiguration configuration, ILogger<OpenAIClient> logger)
    {
        _configuration = configuration;
        _logger = logger;
        var apiKey = _configuration.GetValue<string>("Azure:OpenAIKey");
        if (string.IsNullOrWhiteSpace(apiKey))
        {
            _logger.LogError("openai key is null");
        }
        client.BaseAddress = new Uri("https://api.openai.com/");
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
        Client = client;

    }

    /// <summary>
    /// 生成实体
    /// </summary>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public async Task<List<Choice>?> GetEntityAsync(string name, string? description = null)
    {
        description ??= "实体额外说明:" + description;
        var requestData = new GPTRequest
        {
            Messages = new List<Message> {
                new Message("生成csharp代码"){
                    Role="assistant"
                },
                new Message($"生成【{name}】的实体类型，包含常见业务属性；遵循EF Core 实体类型定义规范；遵循C#11语法特性; 添加类注释和属性注释; 添加必要的索引和字段长度特性"),
                new Message($"注意避免编辑器nullable警告;时间类型使用DateTimeOffset;{description}")
            },
            N = 2
        };
        var response = await Client.PostAsJsonAsync("/v1/chat/completions", requestData);

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadFromJsonAsync<JsonElement>();
            // get [choices field] from data JsonElement
            var choices = data.GetProperty("choices").Deserialize<List<Choice>>();
            return choices;
        }
        return default;
    }

    public class Message
    {
        [JsonPropertyName("role")]
        public string Role { get; set; } = "user";
        [JsonPropertyName("content")]
        public string Content { get; set; } = default!;

        public Message(string content)
        {
            Content = content;
        }
    }

    public class GPTRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = "gpt-3.5-turbo";
        [JsonPropertyName("messages")]
        public List<Message>? Messages { get; set; }
        [JsonPropertyName("n")]
        public int N { get; set; } = 1;
        [JsonPropertyName("max_tokens")]
        public int Max_tokens { get; set; } = 1500;
    }

    public class Choice
    {
        [JsonPropertyName("message")]
        public Message Message { get; set; } = default!;
        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; } = default!;
        [JsonPropertyName("index")]
        public int Index { get; set; } = default!;
    }

}
