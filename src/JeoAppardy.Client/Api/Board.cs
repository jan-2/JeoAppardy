using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace JeoAppardy.Client.Api
{
  public class Board
  {
    public static Board FromJson(string json)
    {
      var board = JsonConvert.DeserializeObject<Board>(json);

      return board;
    }

    public Board()
    {
      this.Categories = new List<Category>();
    }

    public IList<Category> Categories { get; set; }

    public Category FirstCategory
    {
      get { return Categories.First(); }
    }

    public string Title { get; set; }
  }
}