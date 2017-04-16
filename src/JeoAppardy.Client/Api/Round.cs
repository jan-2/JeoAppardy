using System.Collections.Generic;
using System.Linq;

namespace JeoAppardy.Client.Api
{
  public class Round
  {

    public static Round FromJson(string json)
    {

    }

    protected Round()
    {
      this.Categories = new List<Category>();
    }

    public IEnumerable<Category> Categories
    {
      get; set;
    }

    public Category FirstCategory
    {

      get {
        return Categories.First();
      }
    }
  }
}
