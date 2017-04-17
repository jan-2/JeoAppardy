using System;
using System.Collections.Generic;

namespace JeoAppardy.Client.Api
{
  public class Round
  {
    private IList<string> playerNames;

    public Round(Board board)
    {
      playerNames = new List<string>();
      Board = board;
    }

    public Board Board
    {
      get; private set;
    }

    public int Players => playerNames.Count;
  }
}
