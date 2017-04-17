using System.Collections.Generic;
using System.Linq;

namespace JeoAppardy.Client.Api
{
  public class Board
  {

    public static Board FromJson(string json)
    {
      return new Board();
    }

    protected Board()
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
