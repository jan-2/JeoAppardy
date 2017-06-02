﻿using System.Collections.Generic;
using System.Linq;

namespace JeoAppardy.Client.Api
{
  public class GameWall
  {

    public GameWall(string title, IList<Category> categories)
    {
      Title = title;
      Categories = categories.Select(cat => new GameCategory(cat.Title)).ToList();
    }

    public string Title { get; private set; }

    public IList<GameCategory> Categories
    {
      get; private set;
    }

    public bool AllPlayersSet => FirstPlayer != null && SecondPlayer != null && ThirdPlayer != null && FourthPlayer != null;

    public Player FirstPlayer
    {
      get;
      set;
    }

    public Player SecondPlayer
    {
      get;
      set;
    }

    public Player ThirdPlayer
    {
      get;
      set;
    }

    public Player FourthPlayer
    {
      get;
      set;
    }
  }
}
