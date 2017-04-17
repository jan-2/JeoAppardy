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
  public class When_setup_the_Game
  {
    private Game _sut;

    [TestMethod]
    public void It_should_be_something_I_dont_know()
    {
      var firstBoard = Board.FromJson("");
      var secondBoard = Board.FromJson("");
      var thirdBoard = Board.FromJson("");
      var fourthBoard = Board.FromJson("");
      var finalBoard = Board.FromJson("");

      Game.SetupWithBoards(
          firstBoard, secondBoard, thirdBoard, fourthBoard, finalBoard
        );
    }
  }
}
