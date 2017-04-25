using JeoAppardy.Client.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeoAppardy.Tests.GameSetupTests
{
  [TestClass]
  public class When_a_Player_choosed_an_Level
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

      _sut = _game.StartFirstRound();
    }

    private Game SetupFirstRound(Game game)
    {
      var round = game.StartFirstRound();
      round.SetFirstPlayerName("Winner 1");
      round.SetSecondPlayerName("Winner 2");
      round.SetThirdPlayerName("Winner 3");
      round.SetFourthPlayerName("Winner 4");

      game.CurrentRound.PlayerOneWins();

      return game;
    }

    [TestMethod]
    public void It_should_return_the_Answer()
    {
      var answer = _sut.PlayerChoosed(category: 1, level: 100);

      Assert.AreEqual("description 1.1", answer);
    }

    [TestMethod]
    public void And_answered_correct_It_should_return_a_new_GameWall_with_answered_Level()
    {
      var nextGameWall = _sut.FirstPlayerAnsweredCorrect(category: 1, level:100);

      Assert.IsTrue(nextGameWall.Categories[0].Level[0].Solved);

    }

    [TestMethod]
    public void And_answered_correct_It_should_return_a_new_GameWall_with_won_points()
    {
      var nextGameWall = _sut.FirstPlayerAnsweredCorrect(category: 1, level: 100);

      Assert.AreEqual(100, nextGameWall.FirstPlayer.Points);

    }

    [TestMethod]
    public void And_answered_correct_two_times_It_should_return_a_new_GameWall_with_won_points()
    {
      var nextGameWall = _sut.FirstPlayerAnsweredCorrect(category: 1, level: 100);
      nextGameWall = _sut.FirstPlayerAnsweredCorrect(category: 1, level: 200);

      Assert.AreEqual(300, nextGameWall.FirstPlayer.Points);

    }

    [TestMethod]
    public void Answered_correct_choosed_an_other_anser_but_an_other_player_answers_correct_It_should_return_a_new_GameWall_with_Players_won_points()
    {
      var nextGameWall = _sut.FirstPlayerAnsweredCorrect(category: 1, level: 100);
      _sut.PlayerChoosed(category: 2, level: 300);

      nextGameWall = _sut.SecondPlayerAnsweredCorrect(category: 2, level: 300);

      Assert.AreEqual(100, nextGameWall.FirstPlayer.Points);
      Assert.AreEqual(300, nextGameWall.SecondPlayer.Points);

    }
  }
}