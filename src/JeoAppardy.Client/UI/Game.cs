using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using JeoAppardy.Client.Api;
using JeoAppardy.Client.Common;

namespace JeoAppardy.Client.UI
{
  public class Game : ViewModel
  {
    private string _title;
    private Api.Game _gameApi;
    private Api.Round _currentRoundApi;
    private Api.GameWall _currentGameWall;
    private Api.Player _activePlayer;
    private Api.DiscoveredLevel _discoveredLevel;
    private ICommand _setDiscoveredLevelCommand;
    private ObservableCollection<Player> _allPlayers;

    public Game(Api.Game gameApi)
    {
      _gameApi = gameApi;

      this.SetDiscoveredLevelCommand = new DelegateCommand<ItemClickEventArgs>(
        args => SetDiscoveredLevel(args.ClickedItem as Api.GameLevel),
        args => args?.ClickedItem != null);

      // Player fokussieren z.B. Highlight, GameWall aktivieren
      // und "korrekt" "Nicht korrekt" Buttons anzeigen
      // Aktiven Player merken, er wird bei der Beantwortung eines Levels benötigt
    }

    private void SetDiscoveredLevel(Api.GameLevel gameLevel)
    {
      this.DiscoveredLevel = _currentRoundApi.PlayerChoosed(gameLevel.CategoryId, gameLevel.Level);
    }

    public string Title
    {
      get { return _title; }
      set { this.Set(ref _title, value); }
    }

    public Api.Round CurrentRound
    {
      get { return _currentRoundApi; }
      private set { this.Set(ref _currentRoundApi, value); }
    }

    public Api.GameWall CurrentGameWall
    {
      get { return _currentGameWall; }
      set { this.Set(ref _currentGameWall, value); }
    }

    public Api.DiscoveredLevel DiscoveredLevel
    {
      get { return _discoveredLevel; }
      set { this.Set(ref _discoveredLevel, value); }
    }

    public ICommand SetDiscoveredLevelCommand
    {
      get { return _setDiscoveredLevelCommand; }
      set { this.Set(ref _setDiscoveredLevelCommand, value); }
    }

    public ObservableCollection<Api.Player> AllPlayers
    {
      get { return _allPlayers; }
      set { this.Set(ref _allPlayers, value); }
    }

    public Api.Player ActivePlayer
    {
      get { return _activePlayer; }
      set { this.Set(ref _activePlayer, value); }
    }

    public void StartFirstRound()
    {
      SetupCurrentRound(_gameApi.StartFirstRound());
    }

    public void StartSecondRound()
    {
      SetupCurrentRound(_gameApi.StartSecondRound());
    }

    public void StartThirdRound()
    {
      SetupCurrentRound(_gameApi.StartThirdRound());
    }

    public void StartFourthRound()
    {
      SetupCurrentRound(_gameApi.StartFourthRound());
    }

    public void StartFinalRound()
    {
      SetupCurrentRound(_gameApi.StartFinalRound());
    }

    private void SetupCurrentRound(Api.Round currentRound)
    {
      Title = currentRound.GameWall.Title;
      CurrentRound = currentRound;
      CurrentGameWall = CurrentRound.GameWall;
      this.AllPlayers = new ObservableCollection<Player>(new[]
      {
        _gameApi.CurrentRound.FirstPlayer,
        _gameApi.CurrentRound.SecondPlayer,
        _gameApi.CurrentRound.ThirdPlayer,
        _gameApi.CurrentRound.FourthPlayer
      });
    }

    public void SetupPlayerOne(Name name)
    {
      CurrentGameWall = CurrentRound.SetFirstPlayerName(name.Value);
    }

    public void SetupPlayerTwo(Name name)
    {
      CurrentGameWall = CurrentRound.SetSecondPlayerName(name.Value);
    }

    public void SetupPlayerThree(Name name)
    {
      CurrentGameWall = CurrentRound.SetThirdPlayerName(name.Value);
    }

    public void SetupPlayerFour(Name name)
    {
      CurrentGameWall = CurrentRound.SetFourthPlayerName(name.Value);
    }
  }
}