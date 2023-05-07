using System.Text.Json.Serialization;

namespace web_app_rest.Interfaces
{
    public class ErrorInterface
    {
        public class Error
        {
            [JsonPropertyName("message")]
            public string Message { get; set; }
        }
    }
}
