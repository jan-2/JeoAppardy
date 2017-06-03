using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace JeoAppardy.Client.UI
{
  public sealed partial class GameWall : Page
  {
    private Api.Player _activePlayer;

    public GameWall()
    {
      this.InitializeComponent();
    }

    public Game ViewModel { get; private set; }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      this.ViewModel = new Game(e.Parameter as Api.Game);
      this.ViewModel.StartFirstRound();
      if (!this.ViewModel.CurrentGameWall.AllPlayersSet)
      {
        // navigate to Player Eingabe
      }

      base.OnNavigatedTo(e);
    }

    private void firstPlayer_Tapped(object sender, TappedRoutedEventArgs e)
    {
      // Player fokussieren z.B. Highlight, GameWall aktivieren
      // und "korrekt" "Nicht korrekt" Buttons anzeigen

      // Aktiven Player merken, er wird bei der Beantwortung eine Levels benötigt
      _activePlayer = ViewModel.CurrentRound.FirstPlayer;
    }

    private void secondPlayer_Tapped(object sender, TappedRoutedEventArgs e)
    {
      _activePlayer = ViewModel.CurrentRound.SecondPlayer;
    }

    private void thirdPlayer_Tapped(object sender, TappedRoutedEventArgs e)
    {
      _activePlayer = ViewModel.CurrentRound.ThirdPlayer;
    }

    private void fourthPlayer_Tapped(object sender, TappedRoutedEventArgs e)
    {
      _activePlayer = ViewModel.CurrentRound.FourthPlayer;
    }

    private void gameLevels_Tapped(object sender, TappedRoutedEventArgs e)
    {

    }
  }
}