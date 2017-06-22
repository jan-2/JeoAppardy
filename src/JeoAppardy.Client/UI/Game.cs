using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;
using JeoAppardy.Client.Api;
using JeoAppardy.Client.Buzzer;
using JeoAppardy.Client.Common;
using Reactive.Bindings;

namespace JeoAppardy.Client.UI
{
  public class Game : ViewModel
  {
    private IObservable<long> _timer;
    private bool _reset_required;

    private string _title;
    private Api.Game _gameApi;
    private Api.Round _currentRoundApi;
    private Api.GameWall _currentGameWall;
    private Api.Player _activePlayer;
    private Api.DiscoveredLevel _discoveredLevel;

    public Game(Api.Game gameApi)
    {
      // Player fokussieren z.B. Highlight, GameWall aktivieren
      // und "korrekt" "Nicht korrekt" Buttons anzeigen
      // Aktiven Player merken, er wird bei der Beantwortung eines Levels benötigt

      _gameApi = gameApi;

      this.SetDiscoveredLevelCommand = new DelegateCommand<ItemClickEventArgs>(
        args => SetDiscoveredLevel(args.ClickedItem as Api.GameLevel),
        args => CanSetDiscoveredLevel(args.ClickedItem as Api.GameLevel));

      this.CorrectAnswerCommand = new DelegateCommand(
        () => CorrectAnswer(),
        () => DiscoveredLevel != null && this.ActivePlayer != null && DiscoveredLevel.PlayerCanAnswer(ActivePlayer));

      this.WrongAnswerCommand = new DelegateCommand(
        () => WrongAnswer(),
        () => DiscoveredLevel != null && this.ActivePlayer != null && DiscoveredLevel.PlayerCanAnswer(ActivePlayer));

      this.AssetFileLoadedCommand = new DelegateCommand<TextBlock>(
        tb => LoadAssetFileIntoTextBlock(tb),
        tb => tb != null);

      this.DiscardLevelCommand = new DelegateCommand(
        () => DiscardLevel(),
        () => true);

      var scheduler = UIDispatcherScheduler.Default;
      _timer = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(100), scheduler);
      _timer.Subscribe(async _ =>
      {
        if (!Hardware.GetInstance().IsOpen)
        {
          return;
        }

        Hardware.Result result;

        if (_reset_required)
        {
          result = await Hardware.GetInstance().Reset();
          if (result.status == StateEnum.offen)
          {
            _reset_required = false;
            ActivePlayer = null;
          }
        }
        else
        {
          result = await Hardware.GetInstance().GetCurrentState();
        }
        if (result.status == StateEnum.beendet)
        {
          if (AllPlayers.Count < result.sieger)
          {
            // Spieler nicht korrekt konfiguriert oder falscher Drücker verwendet
            return;
          }
          var and_the_winner_is = AllPlayers[result.sieger - 1];
          if (!_reset_required && ActivePlayer != and_the_winner_is && this.DiscoveredLevel != null && this.DiscoveredLevel.PlayerCanAnswer(and_the_winner_is))
          {
            ActivePlayer = and_the_winner_is;
          }
        }
      });
    }

    private async void LoadAssetFileIntoTextBlock(TextBlock tb)
    {
      tb.Text = await GetFileContent(tb.DataContext as string);
    }

    private async Task<string> GetFileContent(string assetFilePath)
    {
      FileInfo fInfo = new FileInfo(assetFilePath);
      if (!fInfo.Exists)
      {
        return ":-(";
      }
      var boardDefinitionFile = await StorageFile.GetFileFromPathAsync(assetFilePath);
      var stream = await boardDefinitionFile.OpenSequentialReadAsync();
      using (StreamReader reader = new StreamReader(stream.AsStreamForRead(), Encoding.UTF8))
      {
        return await reader.ReadToEndAsync();
      }
    }

    private void SetDiscoveredLevel(Api.GameLevel gameLevel)
    {
      _reset_required = true;
      ActivePlayer = null;

      this.DiscoveredLevel = _currentRoundApi.PlayerChoosed(gameLevel.CategoryId, gameLevel.Level);
    }

    private bool CanSetDiscoveredLevel(Api.GameLevel gameLevel)
    {
      return gameLevel != null && !gameLevel.HasBeenAsked;
    }

    private void CorrectAnswer()
    {
      this.CurrentGameWall = this.CurrentRound.PlayerAnsweredCorrect(this.ActivePlayer, this.DiscoveredLevel);
      this.DiscoveredLevel = null;
      this.ActivePlayer = null;
    }

    private void WrongAnswer()
    {
      this.CurrentGameWall = this.CurrentRound.PlayerAnsweredNotCorrect(this.DiscoveredLevel);
      this.DiscoveredLevel.PlayerAnsweredNotCorrect(this.ActivePlayer);
      this.ActivePlayer = null;
      if (this.DiscoveredLevel.AllPlayersAnsweredWrong)
      {
        this.DiscoveredLevel = null;
      }
    }

    private void DiscardLevel()
    {
      this.CurrentGameWall = this.CurrentRound.DiscardLevel(this.DiscoveredLevel);
      this.DiscoveredLevel = null;
      this.ActivePlayer = null;
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
      set
      {
        this.Set(ref _discoveredLevel, value);
        this.CorrectAnswerCommand.RaiseCanExecuteChanged();
        this.WrongAnswerCommand.RaiseCanExecuteChanged();
      }
    }

    public ICommand SetDiscoveredLevelCommand { get; }

    public ICommand AssetFileLoadedCommand { get; }

    public ICommand DiscardLevelCommand { get; }

    public DelegateCommand CorrectAnswerCommand { get; }

    public DelegateCommand WrongAnswerCommand { get; }

    public ObservableCollection<Api.Player> AllPlayers => new ObservableCollection<Player>(new[]
    {
      CurrentGameWall.FirstPlayer,
      CurrentGameWall.SecondPlayer,
      CurrentGameWall.ThirdPlayer,
      CurrentGameWall.FourthPlayer
    });

    public Api.Player ActivePlayer
    {
      get { return _activePlayer; }
      set
      {
        this.Set(ref _activePlayer, value);
        this.CorrectAnswerCommand.RaiseCanExecuteChanged();
        this.WrongAnswerCommand.RaiseCanExecuteChanged();
      }
    }

    public void ContinueCurrentRound()
    {
      SetupCurrentRound(_gameApi.CurrentRound);
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
      this.RaisePropertyChanged(nameof(AllPlayers));
    }

    public void SetupPlayerOne(Name name)
    {
      CurrentGameWall = CurrentRound.SetFirstPlayerName(name.Value);
      this.RaisePropertyChanged(nameof(AllPlayers));
    }

    public void SetupPlayerTwo(Name name)
    {
      CurrentGameWall = CurrentRound.SetSecondPlayerName(name.Value);
      this.RaisePropertyChanged(nameof(AllPlayers));
    }

    public void SetupPlayerThree(Name name)
    {
      CurrentGameWall = CurrentRound.SetThirdPlayerName(name.Value);
      this.RaisePropertyChanged(nameof(AllPlayers));
    }

    public void SetupPlayerFour(Name name)
    {
      CurrentGameWall = CurrentRound.SetFourthPlayerName(name.Value);
      this.RaisePropertyChanged(nameof(AllPlayers));
    }
  }
}