using System.Collections.Generic;

namespace JeoAppardy.Client.Api
{
  public class GameCategory
  {
    public GameCategory(Category category, int id)
    {
      Category = category;
      Id = id;

      Level = new GameLevel[]
      {
        new GameLevel(id, 100),
        new GameLevel(id, 200),
        new GameLevel(id, 300),
        new GameLevel(id, 400)
      };
    }

    public int Id { get; }

    public Category Category { get; }

    public IList<GameLevel> Level { get; }
  }
}