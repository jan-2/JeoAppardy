using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using JeoAppardy.Client.Common;

namespace JeoAppardy.Client.UI
{
  public class Winner : ViewModel
  {
    private readonly Frame _frame;

    public Winner(Frame frame, Api.Game gameApi)
    {
      _frame = frame;
      GameApi = gameApi;

      WinnerImage = new BitmapImage(GameApi.CurrentRound.ID == Api.Game.FINAL
        ? new Uri("ms-appx://JeoAppardy.Client/Assets/chuck-norris.gif")
        : new Uri("ms-appx://JeoAppardy.Client/Assets/dancing.gif"));

      CloseCommand = new DelegateCommand(
        () => _frame.Navigate(typeof(GameWall), GameApi),
        () => true);
    }

    public Api.Game GameApi { get; }

    public ICommand CloseCommand { get; }

    public BitmapImage WinnerImage { get; }
  }
}