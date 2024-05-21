using Newtonsoft.Json;

namespace Dot_NET_Task.Model
{
    public class Question
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
    public class YesNoQuestion : Question
    {
        public bool Answer { get; set; }
    }
    public class DateQuestion : Question
    {
        public DateTime Date { get; set; }
    }
    public class ParagraphQuestion : Question
    {
        public string Answer { get; set; }
    }

    public class NumericQuestion : Question
    {
        public int Answer { get; set; }
    }

    public class DropdownQuestion : Question
    {
        public string[] Choice { get; set; }
        public string Answer { get; set; }
    }
    public class MultipleChoiceQuestion : Question
    {
        public string[] Choice { get; set; }
        public string Answer { get; set; }
        public int MaxChoiceAllowed { get; set; }
    }
}
