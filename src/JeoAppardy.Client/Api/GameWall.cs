using System.Collections.Generic;
using System.Linq;

namespace JeoAppardy.Client.Api
{
  public class GameWall
  {

    public GameWall(IList<Category> categories)
    {
      Categories = categories.Select(cat => new GameCategory(cat.Title)).ToList();
    }

    public string Title => "Round";

    public IList<GameCategory> Categories
    {
      get; private set;
    }

    public Player FirstPlayer
    {
      get;
      set;
    }

    public Player SecondPlayer
    {
      get;
      set;
    }

    public Player ThirdPlayer
    {
      get;
      set;
    }

    public Player FourthPlayer
    {
      get;
      set;
    }
  }
}
