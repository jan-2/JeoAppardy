using System.Collections.Generic;
using System.Linq;

namespace JeoAppardy.Client.Api
{
  public class Category
  {
    public IEnumerable<Answer> Answers
    {
      get; set;
    }

    public Answer FirstAnswer
    {
      get {
        return Answers.First();
      }
    }
  }
}
