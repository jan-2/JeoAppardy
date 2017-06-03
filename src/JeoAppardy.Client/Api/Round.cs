using System;
using System.Collections.Generic;
using System.Linq;

namespace JeoAppardy.Client.Api
{
  public class Round
  {
    private Board _board;

    public Round(Board board)
    {
      _board = board;

      GameWall = new GameWall(board.Title, board.Categories);

      FirstPlayer = new Player();
      SecondPlayer = new Player();
      ThirdPlayer = new Player();
      FourthPlayer = new Player();
    }

    public Player FirstPlayer
    {
      get;
      private set;
    }

    public Player SecondPlayer
    {
      get;
      private set;
    }

    public Player ThirdPlayer
    {
      get;
      private set;
    }

    public Player FourthPlayer
    {
      get;
      private set;
    }

    public Player Winner
    {
      get;
      private set;
    }

    public GameWall SetFirstPlayerName(string playerName)
    {
      FirstPlayer.Name = playerName;

      GameWall.FirstPlayer = FirstPlayer;

      return GameWall;
    }

    public GameWall SetSecondPlayerName(string playerName)
    {
      SecondPlayer.Name = playerName;

      GameWall.SecondPlayer = SecondPlayer;

      return GameWall;
    }

    public GameWall SetThirdPlayerName(string playerName)
    {
      ThirdPlayer.Name = playerName;

      GameWall.ThirdPlayer = ThirdPlayer;

      return GameWall;
    }

    public GameWall SetFourthPlayerName(string playerName)
    {
      FourthPlayer.Name = playerName;

      GameWall.FourthPlayer = FourthPlayer;

      return GameWall;
    }

    public DiscoveredLevel PlayerChoosed(int category, int level)
    {
      var categoryId = Id.AsCategoryId(category);
      var answerId = Id.AsAnswerId(level);
      var asset = _board.Categories[categoryId].Answers[answerId].Description;

      GameWall
        .Categories[categoryId]
        .Level[answerId].HasBeenAsked = true;

      return new DiscoveredLevel(category, level, "text", asset);
    }

    public GameWall PlayerAnsweredCorrect(Player currentPlayer, DiscoveredLevel discoveredLevel)
    {
      SetLevelAsAnwered(discoveredLevel);

      GameWall.ActivePlayer = currentPlayer;

      currentPlayer.Points += discoveredLevel.Level;
      GameWall.ActivePlayer.Points = currentPlayer.Points;

      if (AllAnswersHaveBeenAsked())
        FindTheWinner();

      return GameWall;
    }

    public GameWall PlayerAnsweredNotCorrect(DiscoveredLevel discoveredLevel)
    {
      SetLevelAsNotAnwered(discoveredLevel);

      return GameWall;
    }

    public GameWall GameWall
    {
      get; set;
    }

    private GameWall SetLevelAsAnwered(DiscoveredLevel discoveredLevel)
    {
      GameWall
        .Categories[Id.AsCategoryId(discoveredLevel.Category)]
        .Level[Id.AsAnswerId(discoveredLevel.Level)].Solved = true;

      return GameWall;
    }

    private GameWall SetLevelAsNotAnwered(DiscoveredLevel discoveredLevel)
    {
      GameWall
        .Categories[Id.AsCategoryId(discoveredLevel.Category)]
        .Level[Id.AsAnswerId(discoveredLevel.Level)].Solved = false;

      return GameWall;
    }

    private bool AllAnswersHaveBeenAsked()
    {
      return GameWall.Categories.Any(cat => cat.Level.Any(level => level.HasBeenAsked));
    }

    private void FindTheWinner()
    {
      var allPlayer = new List<Player>() { FirstPlayer, SecondPlayer, ThirdPlayer, FourthPlayer };

      var winner = allPlayer.Aggregate((p1, p2) => p1.Points > p2.Points ? p1 : p2);

      Winner = winner;
    }

    class Id
    {

      public static int AsCategoryId(int category)
      {
        var categoryId = category - 1;

        if (categoryId < 0 || categoryId > 3)
          throw new IndexOutOfRangeException("Categorie darf nur 1, 2, 3 oder 4 sein");

        return categoryId;
      }

      public static int AsAnswerId(int level)
      {
        var answerId = level / 100 - 1;
        if (answerId < 0 || answerId > 3)
          throw new IndexOutOfRangeException("Level darf nur 100, 200, 300 oder 400 sein.");

        return answerId;
      }
    }
  }
}
