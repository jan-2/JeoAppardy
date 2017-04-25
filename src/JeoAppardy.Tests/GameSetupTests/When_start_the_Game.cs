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
  public class When_start_the_Game
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

      _sut = game.StartFirstRound();
    }

    [TestMethod]
    public void It_should_return_the_first_GameWall()
    {
      Assert.AreEqual(firstBoard.Categories[0].Title, _sut.GameWall.Categories[0].Title);
      Assert.AreEqual(firstBoard.Categories[1].Title, _sut.GameWall.Categories[1].Title);
      Assert.AreEqual(firstBoard.Categories[2].Title, _sut.GameWall.Categories[2].Title);
      Assert.AreEqual(firstBoard.Categories[3].Title, _sut.GameWall.Categories[3].Title);
    }
  }
}
