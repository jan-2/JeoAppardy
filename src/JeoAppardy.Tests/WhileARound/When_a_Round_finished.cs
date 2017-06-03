using JeoAppardy.Client.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeoAppardy.Tests.WhileARound
{
  [TestClass]
  public class When_a_Round_finished
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

      _sut = SetupFirstRound(_game);
    }

    private Round SetupFirstRound(Game game)
    {
      var round = game.StartFirstRound();
      round.SetFirstPlayerName("Player 1");
      round.SetSecondPlayerName("Player 2");
      round.SetThirdPlayerName("Player 3");
      round.SetFourthPlayerName("Player 4");

      return round;
    }

    private Round PlayTheRoundTilLastLevel(Round round)
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

      //FourthPlayer spielt nur Level 400 und damit die höchste Punktzahl
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
    public void FourthPlayer_should_be_the_winner()
    {
      _sut = PlayTheRoundTilLastLevel(_sut);

      Assert.AreEqual("Player 4", _sut.Winner.Name);
    }

    [TestMethod]
    public void FourthPlayer_should_have_1600_points()
    {
      _sut = PlayTheRoundTilLastLevel(_sut);

      Assert.AreEqual(1600, _sut.Winner.Points);
    }
  }
}
