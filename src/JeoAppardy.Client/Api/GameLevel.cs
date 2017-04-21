namespace JeoAppardy.Client.Api
{
  public class GameLevel
  {
    public GameLevel(string title)
    {
      Title = title;
    }

    public string Title
    {
      get; private set;
    }

    public bool HasBeenAsked
    {
      get; set;
    }

    public bool Solved
    {
      get; set;
    }
  }
}
