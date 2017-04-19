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
      var rounds = new Dictionary<string, Round> {
        { "first", new Round(first) },
        { "second", new Round(second) },
        { "third", new Round(third) },
        { "fourth", new Round(fourth) },
        { "final", new Round(final) }
      };

      return new Game(rounds);
    }

    protected Game(Dictionary<string, Round> rounds)
    {
      gameRounds = rounds;
    }

    public Round StartFirstRound()
    {
      return gameRounds["first"];
    }

    public Round StartSecondRound()
    {
      return gameRounds["second"];
    }

    public Round StartThirdRound()
    {
      return gameRounds["third"];
    }

    public Round StartFourthRound()
    {
      return gameRounds["fourth"];
    }

    public Round StartFinalRound()
    {
      return gameRounds["final"];
    }
  }
}
