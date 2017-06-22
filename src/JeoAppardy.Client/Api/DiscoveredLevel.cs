using System.Collections.Generic;

namespace JeoAppardy.Client.Api
{
  public class DiscoveredLevel
  {
    private HashSet<Api.Player> wrongAnsweredPlayers = new HashSet<Player>();

    public DiscoveredLevel(GameCategory category, int level, Answer answer)
    {
      Category = category;
      Level = level;
      Answer = answer;
    }

    public GameCategory Category { get; private set; }

    public int Level { get; private set; }

    public Answer Answer { get; private set; }

    public void PlayerAnsweredNotCorrect(Api.Player player)
    {
      wrongAnsweredPlayers.Add(player);
    }

    public bool PlayerCanAnswer(Api.Player player)
    {
      return !wrongAnsweredPlayers.Contains(player);
    }

    public bool AllPlayersAnsweredWrong => wrongAnsweredPlayers.Count == 4;
  }
}