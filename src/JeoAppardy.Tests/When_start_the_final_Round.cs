using JeoAppardy.Client.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JeoAppardy.Tests
{
  [TestClass]
  public class When_start_the_final_Round
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
      game.StartSecondRound();
      game.StartThirdRound();
      game.StartFourthRound();
      _sut = game.StartFinalRound();
    }

    [TestMethod]
    public void It_should_return_the_final_Round()
    {
      Assert.AreEqual(finalBoard, _sut.Board);
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
