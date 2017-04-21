using System.Collections.Generic;

namespace JeoAppardy.Client.Api
{
  public class GameCategory
  {
    public GameCategory(string title)
    {
      Title = title;

      Level = new GameLevel[] {
        new GameLevel("100"),
        new GameLevel("200"),
        new GameLevel("300"),
        new GameLevel("400")
      };
    }

    public string Title
    {
      get; private set;
    }

    public IList<GameLevel> Level
    {
      get; private set;
    }
  }
}
