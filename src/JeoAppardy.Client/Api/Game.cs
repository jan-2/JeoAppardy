using System.Collections.Generic;
using System.Linq;

namespace JeoAppardy.Client.Api
{
  public class Game
  {
    const string FIRST = "first";
    const string SECOND = "second";
    const string THIRD = "third";
    const string FOURTH = "fourth";
    const string FINAL = "final";

    private Dictionary<string, Round> gameRounds;
    private Dictionary<string, Player> winners;

    public static Game SetupWithBoards(Board first, Board second, Board third, Board fourth, Board final)
    {
      var rounds = new Dictionary<string, Round> {
        { FIRST, new Round(FIRST, first) },
        { SECOND, new Round(SECOND, second) },
        { THIRD, new Round(THIRD, third) },
        { FOURTH, new Round(FOURTH, fourth) },
        { FINAL, new Round(FINAL, final) }
      };

      return new Game(rounds);
    }

    protected Game(Dictionary<string, Round> rounds)
    {
      gameRounds = rounds;
      winners = new Dictionary<string, Player>();
    }

    public Round CurrentRound
    {
      get;
      private set;
    }

    private Round SetCurrentRound(string roundName)
    {
      CurrentRound = gameRounds[roundName];

      return CurrentRound;
    }

    public Round StartNextRound(Api.Round round)
    {
      if (round == null)
      {
        return StartFirstRound();
      }

      CurrentRound = round;

      switch (round.ID)
      {
        case FIRST:
          return StartSecondRound();
        case SECOND:
          return StartThirdRound();
        case THIRD:
          return StartFourthRound();
        case FOURTH:
          return StartFinalRound();
        case FINAL:
          return StartFirstRound();
      }

      return CurrentRound;
    }

    public Round StartFirstRound()
    {
      return SetCurrentRound(FIRST);
    }

    public Round StartSecondRound()
    {
      winners.Add(FIRST, CurrentRound.Winner);
      return SetCurrentRound(SECOND);
    }

    public Round StartThirdRound()
    {
      winners.Add(SECOND, CurrentRound.Winner);
      return SetCurrentRound(THIRD);
    }

    public Round StartFourthRound()
    {
      winners.Add(THIRD, CurrentRound.Winner);
      return SetCurrentRound(FOURTH);
    }

    public Round StartFinalRound()
    {
      winners.Add(FOURTH, CurrentRound.Winner);

      var final = SetCurrentRound(FINAL);
      final.SetFirstPlayerName(winners[FIRST].Name);
      final.SetSecondPlayerName(winners[SECOND].Name);
      final.SetThirdPlayerName(winners[THIRD].Name);
      final.SetFourthPlayerName(winners[FOURTH].Name);

      return final;
    }
  }
}
