namespace JeoAppardy.Client.Api
{
  public class DiscoveredLevel
  {
    public DiscoveredLevel(GameCategory category, int level, Answer answer)
    {
      Category = category;
      Level = level;
      Answer = answer;
    }

    public GameCategory Category { get; private set; }

    public int Level { get; private set; }

    public Answer Answer { get; private set; }
  }
}