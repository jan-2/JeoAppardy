using System;
using System.Collections.Generic;
using System.Linq;

namespace JeoAppardy.Client.Api
{
  public class Round
  {
    public Round(Board board)
    {
      Board = board;

      FirstPlayer = new Player();
      SecondPlayer = new Player();
      ThirdPlayer = new Player();
      FourthPlayer = new Player();
    }

    public Board Board
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

    public void SetFirstPlayerName(string playerName)
    {
      FirstPlayer.Name = playerName;
    }

    public void SetSecondPlayerName(string playerName)
    {
      SecondPlayer.Name = playerName;
    }

    public void SetThirdPlayerName(string playerName)
    {
      ThirdPlayer.Name = playerName;
    }

    public void SetFourthPlayerName(string playerName)
    {
      FourthPlayer.Name = playerName;
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
  }
}
