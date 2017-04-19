using JeoAppardy.Client.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JeoAppardy.Tests
{
  [TestClass]
  public class When_start_the_second_Round
  {
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

      var game = Game.SetupWithBoards(
          firstBoard, secondBoard, thirdBoard, fourthBoard, finalBoard
        );

      game.StartFirstRound();
      _sut = game.StartSecondRound();
    }

    [TestMethod]
    public void It_should_return_the_second_Round()
    {
      Assert.AreEqual(secondBoard, _sut.Board);
    }

    [TestMethod]
    public void It_does_not_contain_a_player()
    {
      Assert.AreEqual(string.Empty, _sut.FirstPlayer.Name);
      Assert.AreEqual(string.Empty, _sut.SecondPlayer.Name);
      Assert.AreEqual(string.Empty, _sut.ThirdPlayer.Name);
      Assert.AreEqual(string.Empty, _sut.FourthPlayer.Name);
    }
  }
}
