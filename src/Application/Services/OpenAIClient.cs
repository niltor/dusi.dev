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
            Messages = [
                new Message("生成csharp代码"){
                    Role="assistant"
                },
                new Message($"生成【{name}】的实体类型，包含常见业务属性；遵循 EF Core 实体类定义规范；使用C#11语法; 添加类注释和属性注释; 添加字段长度特性"),

                new Message("不包含主键Id属性;不添加[Required]特性;不包含命名空间代码"){
                    Role="assistant"
                },
                new Message($"时间类型使用DateTimeOffset;{description}")
            ],
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

    /// <summary>
    /// 对话
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public async Task<List<Choice>?> ResponseChatAsync(string content)
    {
        var requestData = new GPTRequest
        {
            Messages = [
                new Message("You are a wise and rational polymath who enjoys chatting with other people, your name is freedom, and You are simulating a real human being having a conversation!") {
                    Role = "system"
                },
                new Message("content"),
            ],
            N = 1,
            Max_tokens = 100,
            Temperature = 0.1
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
        [JsonPropertyName("temperature")]
        public double Temperature { get; set; } = 1;
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
