using JeoAppardy.Client.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JeoAppardy.Tests
{
  [TestClass]
  public class When_setup_the_Player
  {
    private Round _sut;
    private Board oneBoard;

    [TestInitialize]
    public void Setup()
    {
      oneBoard = Board.FromJson(TestData.OneBoard);

      var game = Game.SetupWithBoards(
          oneBoard, oneBoard, oneBoard, oneBoard, oneBoard
        );

      _sut = game.StartFirstRound();
    }

    [TestMethod]
    public void After_add_first_Player_the_name_of_player_are_equal()
    {
      _sut.SetFirstPlayerName("Heinz");

      Assert.AreEqual("Heinz", _sut.FirstPlayer.Name);
    }

    [TestMethod]
    public void After_add_second_Player_the_name_of_player_are_equal()
    {
      _sut.SetSecondPlayerName("Heinz");

      Assert.AreEqual("Heinz", _sut.SecondPlayer.Name);
    }

    [TestMethod]
    public void After_add_third_Player_the_name_of_player_are_equal()
    {
      _sut.SetThirdPlayerName("Heinz");

      Assert.AreEqual("Heinz", _sut.ThirdPlayer.Name);
    }

    [TestMethod]
    public void After_add_fourth_Player_the_name_of_player_are_equal()
    {
      _sut.SetFourthPlayerName("Heinz");

      Assert.AreEqual("Heinz", _sut.FourthPlayer.Name);
    }

    [TestMethod]
    public void After_adding_two_players_and_change_first_Player_the_name_of_the_Player_will_changed()
    {
      _sut.SetFirstPlayerName("Heinz");
      _sut.SetSecondPlayerName("Heinzilein");
      _sut.SetFirstPlayerName("Heinzileinchen");

      Assert.AreEqual("Heinzileinchen", _sut.FirstPlayer.Name);
    }
  }
}
