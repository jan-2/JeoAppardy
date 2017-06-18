namespace JeoAppardy.Client.Api
{
  public class GameLevel
  {
    public GameLevel(int categoryId, int level)
    {
      CategoryId = categoryId;
      Level = level;
    }

    public int CategoryId { get; }
    public int Level { get; }

    public bool HasBeenAsked { get; set; }

    public bool Solved { get; set; }
  }
}