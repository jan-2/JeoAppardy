using Newtonsoft.Json;

namespace JeoAppardy.Client.Api
{
  public class Answer
  {
    public string Asset { get; set; }

    public AnswerType Type { get; set; }

    [JsonProperty(PropertyName = "related_question")]
    public string RelatedQuestion { get; set; }
  }
}