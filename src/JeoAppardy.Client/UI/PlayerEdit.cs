using JeoAppardy.Client.Common;

namespace JeoAppardy.Client.UI
{
  public class PlayerEdit : ViewModel
  {
    public PlayerEdit(Api.Round round)
    {
      this.Round = round;
    }

    public Api.Round Round { get; set; }
  }
}