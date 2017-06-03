using JeoAppardy.Client.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JeoAppardy.Tests.GameSetupTests
{
  [TestClass]
  public class When_start_the_final_Round
  {
    private Game _game;
    private Round _sut;
    private Board firstBoard;
    private Board secondBoard;
    private Board thirdBoard;
    private Board fourthBoard;
    private Board finalBoard;

    [TestInitialize]
    public void Setup()
    {
      firstBoard = Board.FromJson(TestData.FirstBoard);
      secondBoard = Board.FromJson(TestData.SecondBoard);
      thirdBoard = Board.FromJson(TestData.ThirdBoard);
      fourthBoard = Board.FromJson(TestData.FourthBoard);
      finalBoard = Board.FromJson(TestData.FinalBoard);

      _game = Game.SetupWithBoards(
          firstBoard, secondBoard, thirdBoard, fourthBoard, finalBoard
        );

      _game = SetupFirstRound(_game);
      _game = SetupSecondRound(_game);
      _game = SetupThirdRound(_game);
      _game = SetupFourthRound(_game);

      _sut = _game.StartFinalRound();
    }

    private Game SetupFirstRound(Game game)
    {
      var round = game.StartFirstRound();
      round.SetFirstPlayerName("Winner 1");
      round.SetSecondPlayerName("Winner 2");
      round.SetThirdPlayerName("Winner 3");
      round.SetFourthPlayerName("Winner 4");

      PlayFirstRoundWhereFirstPlayerWins(round);

      return game;
    }

    private Game SetupSecondRound(Game game)
    {
      var round = game.StartSecondRound();
      round.SetFirstPlayerName("Winner 1");
      round.SetSecondPlayerName("Winner 2");
      round.SetThirdPlayerName("Winner 3");
      round.SetFourthPlayerName("Winner 4");

      PlaySecoundRoundWhereSecondPlayerWins(round);

      return game;
    }

    private Game SetupThirdRound(Game game)
    {
      var round = game.StartThirdRound();
      round.SetFirstPlayerName("Winner 1");
      round.SetSecondPlayerName("Winner 2");
      round.SetThirdPlayerName("Winner 3");
      round.SetFourthPlayerName("Winner 4");

      PlayThirdRoundWhereThirdPlayerWins(round);

      return game;
    }

    private Game SetupFourthRound(Game game)
    {
      var round = game.StartFourthRound();
      round.SetFirstPlayerName("Winner 1");
      round.SetSecondPlayerName("Winner 2");
      round.SetThirdPlayerName("Winner 3");
      round.SetFourthPlayerName("Winner 4");

      PlayFourthRoundWhereFourthPlayerWins(round);

      return game;
    }

    private Round PlayFirstRoundWhereFirstPlayerWins(Round round)
    {
      DiscoveredLevel discoveredLevel;

      //FirstPlayer spielt nur Level 400
      discoveredLevel = round.PlayerChoosed(1, 400);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 400);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 400);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 400);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      //SecondPlayer spielt nur Level 200
      discoveredLevel = round.PlayerChoosed(1, 200);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 200);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 200);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 200);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      //ThirdPlayer speilt nur Level 300
      discoveredLevel = round.PlayerChoosed(1, 300);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 300);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 300);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 300);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      //FourthPlayer spielt nur Level 100
      discoveredLevel = round.PlayerChoosed(1, 100);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 100);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 100);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 100);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      return round;
    }

    private Round PlaySecoundRoundWhereSecondPlayerWins(Round round)
    {
      DiscoveredLevel discoveredLevel;

      //FirstPlayer spielt nur Level 100
      discoveredLevel = round.PlayerChoosed(1, 100);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 100);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 100);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 100);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      //SecondPlayer spielt nur Level 400
      discoveredLevel = round.PlayerChoosed(1, 400);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 400);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 400);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 400);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      //ThirdPlayer speilt nur Level 300
      discoveredLevel = round.PlayerChoosed(1, 300);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 300);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 300);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 300);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      //FourthPlayer spielt nur Level 200
      discoveredLevel = round.PlayerChoosed(1, 200);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 200);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 200);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 200);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      return round;
    }

    private Round PlayThirdRoundWhereThirdPlayerWins(Round round)
    {
      DiscoveredLevel discoveredLevel;

      //FirstPlayer spielt nur Level 100
      discoveredLevel = round.PlayerChoosed(1, 100);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 100);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 100);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 100);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      //SecondPlayer spielt nur Level 200
      discoveredLevel = round.PlayerChoosed(1, 200);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 200);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 200);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 200);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      //ThirdPlayer speilt nur Level 400
      discoveredLevel = round.PlayerChoosed(1, 400);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 400);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 400);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 400);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      //FourthPlayer spielt nur Level 300
      discoveredLevel = round.PlayerChoosed(1, 300);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 300);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 300);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 300);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      return round;
    }

    private Round PlayFourthRoundWhereFourthPlayerWins(Round round)
    {
      DiscoveredLevel discoveredLevel;

      //FirstPlayer spielt nur Level 100
      discoveredLevel = round.PlayerChoosed(1, 100);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 100);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 100);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 100);
      round.PlayerAnsweredCorrect(round.FirstPlayer, discoveredLevel);

      //SecondPlayer spielt nur Level 200
      discoveredLevel = round.PlayerChoosed(1, 200);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 200);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 200);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 200);
      round.PlayerAnsweredCorrect(round.SecondPlayer, discoveredLevel);

      //ThirdPlayer speilt nur Level 300
      discoveredLevel = round.PlayerChoosed(1, 300);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 300);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 300);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 300);
      round.PlayerAnsweredCorrect(round.ThirdPlayer, discoveredLevel);

      //FourthPlayer spielt nur Level 400
      discoveredLevel = round.PlayerChoosed(1, 400);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(2, 400);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(3, 400);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      discoveredLevel = round.PlayerChoosed(4, 400);
      round.PlayerAnsweredCorrect(round.FourthPlayer, discoveredLevel);

      return round;
    }

    [TestMethod]
    public void It_does_contain_the_winner_of_all_previous_rounds()
    {
      Assert.AreEqual("Winner 1", _sut.FirstPlayer.Name);
      Assert.AreEqual("Winner 2", _sut.SecondPlayer.Name);
      Assert.AreEqual("Winner 3", _sut.ThirdPlayer.Name);
      Assert.AreEqual("Winner 4", _sut.FourthPlayer.Name);
    }
  }
}