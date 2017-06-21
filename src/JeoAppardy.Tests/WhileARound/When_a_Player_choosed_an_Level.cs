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

    [TestMethod]
    public void It_should_return_the_Answer()
    {
      var discoveredLevel = _sut.PlayerChoosed(category: 1, level: 100);

      Assert.IsNotNull(discoveredLevel.Answer);
      Assert.AreEqual("description 1.1", discoveredLevel.Answer.Asset);
    }

    [TestMethod]
    public void And_answered_correct_It_should_return_a_new_GameWall_with_answered_Level()
    {
      var discoveredLevel = _sut.PlayerChoosed(category: 1, level: 100);
      var nextGameWall = _sut.PlayerAnsweredCorrect(_sut.FirstPlayer, discoveredLevel);

      Assert.IsTrue(nextGameWall.Categories[0].Level[0].Solved);

    }

    [TestMethod]
    public void And_answered_correct_It_should_return_a_new_GameWall_with_marked_as_asked_Level()
    {
      var discoveredLevel = _sut.PlayerChoosed(category: 1, level: 100);
      var nextGameWall = _sut.PlayerAnsweredCorrect(_sut.FirstPlayer, discoveredLevel);

      Assert.IsTrue(nextGameWall.Categories[0].Level[0].HasBeenAsked);

    }

    [TestMethod]
    public void And_answered_correct_It_should_return_a_new_GameWall_with_won_points()
    {
      var discoveredLevel = _sut.PlayerChoosed(category: 1, level: 100);
      var nextGameWall = _sut.PlayerAnsweredCorrect(_sut.FirstPlayer, discoveredLevel);

      Assert.AreEqual(100, nextGameWall.FirstPlayer.Points);

    }

    [TestMethod]
    public void And_answered_correct_two_times_It_should_return_a_new_GameWall_with_won_points()
    {
      var discoveredLevel1 = _sut.PlayerChoosed(category: 1, level: 100);
      var nextGameWall = _sut.PlayerAnsweredCorrect(_sut.FirstPlayer, discoveredLevel1);

      var discoveredLevel2 = _sut.PlayerChoosed(category: 1, level: 200);
      nextGameWall = _sut.PlayerAnsweredCorrect(_sut.FirstPlayer, discoveredLevel2);

      Assert.AreEqual(300, nextGameWall.FirstPlayer.Points);

    }

    [TestMethod]
    public void Answered_correct_choosed_an_other_anser_but_an_other_player_answers_correct_It_should_return_a_new_GameWall_with_Players_won_points()
    {
      var discoveredLevel1 = _sut.PlayerChoosed(category: 1, level: 100);
      var gameWall = _sut.PlayerAnsweredCorrect(_sut.FirstPlayer, discoveredLevel1);

      var discoveredLevel2 = _sut.PlayerChoosed(category: 2, level: 300);
      gameWall = _sut.PlayerAnsweredNotCorrect(discoveredLevel2);

      var discoveredLevel3 = _sut.PlayerChoosed(category: 2, level: 300);
      gameWall = _sut.PlayerAnsweredCorrect(_sut.SecondPlayer, discoveredLevel3);

      Assert.AreEqual(100, gameWall.FirstPlayer.Points);
      Assert.AreEqual(300, gameWall.SecondPlayer.Points);

    }

    [TestMethod]
    public void And_three_players_play_the_game()
    {
      var discoveredLevel1 = _sut.PlayerChoosed(category: 1, level: 100);
      var gameWall = _sut.PlayerAnsweredCorrect(_sut.FirstPlayer, discoveredLevel1);

      var discoveredLevel2 = _sut.PlayerChoosed(category: 2, level: 300);
      gameWall = _sut.PlayerAnsweredNotCorrect(discoveredLevel2);

      gameWall = _sut.PlayerAnsweredCorrect(_sut.SecondPlayer, discoveredLevel2);

      var discoveredLevel3 = _sut.PlayerChoosed(category: 3, level: 200);
      gameWall = _sut.PlayerAnsweredCorrect(_sut.SecondPlayer, discoveredLevel3);

      var discoveredLevel4 = _sut.PlayerChoosed(category: 4, level: 400);
      gameWall = _sut.PlayerAnsweredNotCorrect(discoveredLevel4);

      gameWall = _sut.PlayerAnsweredCorrect(_sut.ThirdPlayer, discoveredLevel4);

      var discoveredLevel5 = _sut.PlayerChoosed(category: 4, level: 300);
      gameWall = _sut.PlayerAnsweredNotCorrect(discoveredLevel5);

      gameWall = _sut.PlayerAnsweredCorrect(_sut.FirstPlayer, discoveredLevel5);

      Assert.AreEqual(400, gameWall.FirstPlayer.Points);
      Assert.AreEqual(500, gameWall.SecondPlayer.Points);
      Assert.AreEqual(400, gameWall.ThirdPlayer.Points);

    }

    [TestMethod]
    public void And_answered_not_correct_It_should_return_a_new_GameWall_with_marked_as_asked_Level()
    {
      var discoveredLevel = _sut.PlayerChoosed(category: 1, level: 100);
      var gameWall = _sut.PlayerAnsweredCorrect(_sut.FirstPlayer, discoveredLevel);

      Assert.IsTrue(gameWall.Categories[0].Level[0].HasBeenAsked);

    }
  }
}