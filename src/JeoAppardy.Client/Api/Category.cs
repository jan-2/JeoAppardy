using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace JeoAppardy.Client.Api
{
  public class Category
  {
    public string Title
    {
      get; set;
    }

    public IList<Answer> Answers
    {
      get; set;
    }

    [JsonIgnore]
    public Answer FirstAnswer
    {
      get {
        return Answers.First();
      }
    }
  }
}
