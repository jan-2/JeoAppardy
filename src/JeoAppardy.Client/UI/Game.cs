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
    private ICommand _setActivePlayerCommand;
    private Api.DiscoveredLevel _discoveredLevel;
    private ICommand _setDiscoveredLevelCommand;

    public Game(Api.Game gameApi)
    {
      _gameApi = gameApi;

      this.SetDiscoveredLevelCommand = new DelegateCommand<ItemClickEventArgs>(
        SetDiscoveredLevel,
        args => args?.ClickedItem != null && this.ActivePlayer != null);

      // Player fokussieren z.B. Highlight, GameWall aktivieren
      // und "korrekt" "Nicht korrekt" Buttons anzeigen
      // Aktiven Player merken, er wird bei der Beantwortung eines Levels benötigt
      this.SetActivePlayerCommand = new DelegateCommand<Api.Player>(
        player => this.ActivePlayer = player,
        player => true);
    }

    private void SetDiscoveredLevel(ItemClickEventArgs args)
    {
      //this.DiscoveredLevel = _currentRoundApi.PlayerChoosed(0, 0);
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

    public Api.Player ActivePlayer
    {
      get { return _activePlayer; }
      set { this.Set(ref _activePlayer, value); }
    }

    public ICommand SetActivePlayerCommand
    {
      get { return _setActivePlayerCommand; }
      set { this.Set(ref _setActivePlayerCommand, value); }
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