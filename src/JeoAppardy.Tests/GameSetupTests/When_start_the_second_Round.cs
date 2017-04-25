using JeoAppardy.Client.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JeoAppardy.Tests.GameSetupTests
{
  [TestClass]
  public class When_start_the_second_Round
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

      _game.StartFirstRound();
      _sut = _game.StartSecondRound();
    }

    [TestMethod]
    public void It_does_not_contain_a_player()
    {
      Assert.IsNull(_sut.FirstPlayer.Name);
      Assert.IsNull(_sut.SecondPlayer.Name);
      Assert.IsNull(_sut.ThirdPlayer.Name);
      Assert.IsNull(_sut.FourthPlayer.Name);
    }
  }
}
