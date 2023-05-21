using System.Text.Json.Serialization;

namespace PlayWriteApplication
{
    public  class GptSend
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("messages")]
        public Message[] Messages { get; set; }
    }

    //public partial class Message
    //{
    //    [JsonPropertyName("role")]
    //    public string Role { get; set; }

    //    [JsonPropertyName("content")]
    //    public string Content { get; set; }
    //}
}
