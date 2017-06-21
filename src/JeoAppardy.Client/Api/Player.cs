using System;
using JeoAppardy.Client.Common;

namespace JeoAppardy.Client.Api
{
  public class Player : Notifyable
  {
    private int _points;

    public String Name { get; set; }

    public int Points
    {
      get { return _points; }
      set { this.Set(ref _points, value); }
    }
  }
}