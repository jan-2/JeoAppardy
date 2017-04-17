using JeoAppardy.Client.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeoAppardy.Tests
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
      firstBoard = Board.FromJson("");
      secondBoard = Board.FromJson("");
      thirdBoard = Board.FromJson("");
      fourthBoard = Board.FromJson("");
      finalBoard = Board.FromJson("");

      var game = Game.SetupWithBoards(
          firstBoard, secondBoard, thirdBoard, fourthBoard, finalBoard
        );

      _sut = game.Start();
    }

    [TestMethod]
    public void It_should_return_the_first_Round()
    {
      Assert.AreEqual(firstBoard, _sut.Board);
    }
    
    [TestMethod]
    public void It_does_not_contain_a_player()
    {
      Assert.AreEqual(0, _sut.Players);
    }
  }
}
