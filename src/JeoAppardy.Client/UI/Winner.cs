using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using JeoAppardy.Client.Api;
using JeoAppardy.Client.Common;

namespace JeoAppardy.Client.UI
{
  public class Winner : ViewModel
  {
    private readonly Frame _frame;
    private Player _realWinner;

    public Winner(Frame frame, Api.Game gameApi)
    {
      _frame = frame;
      GameApi = gameApi;

      WinnerImage = new BitmapImage(GameApi.CurrentRound.ID == Api.Game.FINAL
        ? new Uri("ms-appx://JeoAppardy.Client/Assets/chuck-norris.gif")
        : new Uri("ms-appx://JeoAppardy.Client/Assets/dancing.gif"));

      CloseCommand = new DelegateCommand(
        () => Close(),
        () => this.RealWinner != null);

      this.RealWinner = gameApi.CurrentRound.Winner;
    }

    private void Close()
    {
      GameApi.CurrentRound.Winner = this.RealWinner;
      _frame.Navigate(typeof(GameWall), GameApi);
    }

    public Api.Game GameApi { get; }

    public DelegateCommand CloseCommand { get; }

    public BitmapImage WinnerImage { get; }

    public Api.Player RealWinner
    {
      get { return _realWinner; }
      set
      {
        this.Set(ref _realWinner, value);
        CloseCommand.RaiseCanExecuteChanged();
      }
    }
  }
}