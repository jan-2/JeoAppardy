using Newtonsoft.Json;

namespace JeoAppardy.Client.Api
{
  public class Answer
  {
    public string Description
    {
      get; set;
    }
    [JsonProperty(PropertyName = "related_question")]
    public string RelatedQuestion
    {
      get; set;
    }
  }
}
