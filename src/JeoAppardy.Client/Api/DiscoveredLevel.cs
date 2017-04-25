namespace JeoAppardy.Client.Api
{
  public class DiscoveredLevel
  {
    public DiscoveredLevel(int category, int level, string type, string asset)
    {
      Category = category;
      Level = level;
      Type = type;
      Asset = asset;
    }

    public int Category
    {
      get; private set;
    }
    public int Level
    {
      get; private set;
    }
    public string Type
    {
      get; private set;
    }
    public string Asset
    {
      get; private set;
    }
  }
}
