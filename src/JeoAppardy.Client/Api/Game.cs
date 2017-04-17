using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeoAppardy.Client.Api
{
  public class Game
  {
    private Dictionary<string, Round> gameRounds;

    public static Game SetupWithBoards(Board first, Board second, Board third, Board fourth, Board final)
    {
      var rounds = new Dictionary<string, Round>();
      rounds.Add("first", new Round(first));
      rounds.Add("second", new Round(second));
      rounds.Add("third", new Round(third));
      rounds.Add("fourth", new Round(fourth));
      rounds.Add("final", new Round(final));

      return new Game(rounds);
    }

    protected Game(Dictionary<string, Round> rounds)
    {
      gameRounds = rounds;
    }

    public Round Start()
    {
      return gameRounds["first"];
    }
  }
}
