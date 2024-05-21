using Newtonsoft.Json;

namespace Dot_NET_Task.DTO
{
    public class QuestionDTO
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
