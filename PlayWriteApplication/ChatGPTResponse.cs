using System.Text.Json.Serialization;

namespace PlayWriteApplication
{
    public class ChatGPTResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("created")]
        public long Created { get; set; }

        [JsonPropertyName("choices")]
        public Choice[] Choices { get; set; }

        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }

    }

    public partial class Choice
    {
        [JsonPropertyName("index")]
        public long Index { get; set; }

        [JsonPropertyName("message")]
        public Message Message { get; set; }

        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
    }

    public partial class Message
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }

    public partial class Usage
    {
        [JsonPropertyName("prompt_tokens")]
        public long PromptTokens { get; set; }

        [JsonPropertyName("completion_tokens")]
        public long CompletionTokens { get; set; } = 4000;

        [JsonPropertyName("total_tokens")]
        public long TotalTokens { get; set; } = 4000;
    }
}


