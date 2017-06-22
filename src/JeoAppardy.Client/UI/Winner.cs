using System.Windows.Input;
using Windows.UI.Xaml.Controls;
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

      CloseCommand = new DelegateCommand(
        () => _frame.Navigate(typeof(GameWall), GameApi),
        () => true);
    }

    public Api.Game GameApi { get; }

    public ICommand CloseCommand { get; }
  }
}