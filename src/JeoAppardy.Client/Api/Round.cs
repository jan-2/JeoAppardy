using System;
using System.Linq;

namespace JeoAppardy.Client.Api
{
  public class Round
  {
    private Board _board;

    public Round(Board board)
    {
      _board = board;

      GameWall = new GameWall(board.Categories);

      FirstPlayer = new Player();
      SecondPlayer = new Player();
      ThirdPlayer = new Player();
      FourthPlayer = new Player();
    }

    public GameWall GameWall
    {
      get; private set;
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

      GameWall.ThirdPlayer = FourthPlayer;

      return GameWall;
    }

    public void PlayerOneWins()
    {
      Winner = FirstPlayer;
    }

    public void PlayerTwoWins()
    {
      Winner = SecondPlayer;
    }

    public void PlayerThreeWins()
    {
      Winner = ThirdPlayer;
    }

    public void PlayerFourWins()
    {
      Winner = FourthPlayer;
    }

    public string PlayerChoosed(int category, int level)
    {
      var ids = new Ids(category, level);

      GameWall
        .Categories[ids.CategoryId]
        .Level[ids.AnswerId].HasBeenAsked = true;

      return _board
        .Categories[ids.CategoryId]
        .Answers[ids.AnswerId]
        .Description;
    }

    public GameWall FirstPlayerAnsweredCorrect(int category, int level)
    {
      var ids = new Ids(category, level);

      GameWall
        .Categories[ids.CategoryId]
        .Level[ids.AnswerId].Solved = true;

      FirstPlayer.Points += level;
      GameWall.FirstPlayer.Points = FirstPlayer.Points;

      return GameWall;
    }

    public GameWall SecondPlayerAnsweredCorrect(int category, int level)
    {
      var ids = new Ids(category, level);

      GameWall
        .Categories[ids.CategoryId]
        .Level[ids.AnswerId].Solved = true;

      SecondPlayer.Points += level;
      GameWall.SecondPlayer.Points = SecondPlayer.Points;

      return GameWall;
    }

    public GameWall ThirdPlayerAnsweredCorrect(int category, int level)
    {
      var ids = new Ids(category, level);

      GameWall
        .Categories[ids.CategoryId]
        .Level[ids.AnswerId].Solved = true;

      ThirdPlayer.Points += level;
      GameWall.ThirdPlayer.Points = ThirdPlayer.Points;

      return GameWall;
    }

    public GameWall FourthPlayerAnsweredCorrect(int category, int level)
    {
      var ids = new Ids(category, level);

      GameWall
        .Categories[ids.CategoryId]
        .Level[ids.AnswerId].Solved = true;

      FourthPlayer.Points += level;
      GameWall.FourthPlayer.Points = FourthPlayer.Points;

      return GameWall;
    }

    class Ids
    {
      private int categoryId;
      private int answerId;

      public Ids(int category, int level)
      {
        SetCategoryId(category);
        SetAnswerId(level);
      }

      private void SetCategoryId(int category)
      {
        categoryId = category - 1;
        if (categoryId < 0 || categoryId > 3)
          throw new IndexOutOfRangeException("Categorie darf nur 1, 2, 3 oder 4 sein");
      }
      public int CategoryId
      {
        get {
          return categoryId;
        }
      }

      private void SetAnswerId(int level)
      {
        answerId = level / 100 - 1;
        if (answerId < 0 || answerId > 3)
          throw new IndexOutOfRangeException("Level darf nur 100, 200, 300 oder 400 sein.");
      }
      public int AnswerId
      {
        get {
          return answerId;
        }
      }
    }
  }
}
