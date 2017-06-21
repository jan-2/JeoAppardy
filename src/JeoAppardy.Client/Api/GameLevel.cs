using JeoAppardy.Client.Common;

namespace JeoAppardy.Client.Api
{
  public class GameLevel : Notifyable
  {
    private bool _hasBeenAsked;
    private bool _solved;

    public GameLevel(int categoryId, int level)
    {
      CategoryId = categoryId;
      Level = level;
    }

    public int CategoryId { get; }

    public int Level { get; }

    public bool HasBeenAsked
    {
      get { return _hasBeenAsked; }
      set { this.Set(ref _hasBeenAsked, value); }
    }

    public bool Solved
    {
      get { return _solved; }
      set { this.Set(ref _solved, value); }
    }
  }
}