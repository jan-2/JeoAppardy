﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using JeoAppardy.Client.Api;
using JeoAppardy.Client.Buzzer;
using JeoAppardy.Client.Common;
using Reactive.Bindings;

namespace JeoAppardy.Client.UI
{
  public class Game : ViewModel
  {
    private IObservable<long> _timer;

    private Frame _frame;
    private string _title;
    private Api.Game _gameApi;
    private Api.Round _currentRoundApi;
    private Api.GameWall _currentGameWall;
    private Api.Player _activePlayer;
    private Api.DiscoveredLevel _discoveredLevel;
    private Api.Answer _relatedQuestion;

    public Game(Frame frame, Api.Game gameApi)
    {
      _frame = frame;
      _gameApi = gameApi;

      this.SetDiscoveredLevelCommand = new DelegateCommand<ItemClickEventArgs>(
        args => SetDiscoveredLevel(args.ClickedItem as Api.GameLevel),
        args => CanSetDiscoveredLevel(args.ClickedItem as Api.GameLevel));

      this.CloseRelatedQuestionCommand = new DelegateCommand(
        () => CloseRelatedQuestion(),
        () => true);

      this.CorrectAnswerCommand = new DelegateCommand(
        () => CorrectAnswer(),
        () => DiscoveredLevel != null && this.ActivePlayer != null && DiscoveredLevel.PlayerCanAnswer(ActivePlayer));

      this.WrongAnswerCommand = new DelegateCommand(
        () => WrongAnswer(),
        () => DiscoveredLevel != null && this.ActivePlayer != null && DiscoveredLevel.PlayerCanAnswer(ActivePlayer));

      this.NoOneWantAnswerCommand = new DelegateCommand(
        () => NoOneWantAnswer(),
        () => DiscoveredLevel != null);

      this.AssetFileLoadedCommand = new DelegateCommand<TextBlock>(
        tb => LoadAssetFileIntoTextBlock(tb),
        tb => tb != null);

      this.AssetImageLoadedCommand = new DelegateCommand<Image>(
        img => LoadAssetFileIntoImage(img),
        img => img != null);

      this.DiscardLevelCommand = new DelegateCommand(
        () => DiscardLevel(),
        () => true);

    }

    private async void LoadAssetFileIntoTextBlock(TextBlock tb)
    {
      tb.Text = await GetFileContent(tb.DataContext as string);
    }

    private async Task<string> GetFileContent(string assetFilePath)
    {
      var file = await StorageFile.GetFileFromPathAsync(assetFilePath);
      if (file == null)
      {
        return ":-(";
      }
      var stream = await file.OpenSequentialReadAsync();
      using (StreamReader reader = new StreamReader(stream.AsStreamForRead(), Encoding.UTF8))
      {
        return await reader.ReadToEndAsync();
      }
    }

    private async void LoadAssetFileIntoImage(Image img)
    {
      img.Source = null;
      var file = await StorageFile.GetFileFromPathAsync(img.DataContext as string);
      if (file != null)
      {
        img.Source = await LoadImage(file);
      }
    }

    private static async Task<BitmapImage> LoadImage(StorageFile file)
    {
      var bitmapImage = new BitmapImage();
      var stream = await file.OpenAsync(FileAccessMode.Read);
      bitmapImage.SetSource(stream);
      return bitmapImage;
    }

    private void SetDiscoveredLevel(Api.GameLevel gameLevel)
    {
      ActivePlayer = null;

      this.DiscoveredLevel = _currentRoundApi.PlayerChoosed(gameLevel.CategoryId, gameLevel.Level);
    }

    private bool CanSetDiscoveredLevel(Api.GameLevel gameLevel)
    {
      return gameLevel != null && !gameLevel.HasBeenAsked && this.DiscoveredLevel == null;
    }

    private void CloseRelatedQuestion()
    {
      this.RelatedQuestion = null;
      this.DiscoveredLevel = null;
      this.ActivePlayer = null;

      if (this.CurrentRound.Winner != null)
      {
        // start next round
        _frame.Navigate(typeof(WinnerView), _gameApi);
      }
    }

    private void CorrectAnswer()
    {
      this.CurrentGameWall = this.CurrentRound.PlayerAnsweredCorrect(this.ActivePlayer, this.DiscoveredLevel);
      this.RelatedQuestion = this.DiscoveredLevel.Answer;
    }

    private void WrongAnswer()
    {
      this.CurrentGameWall = this.CurrentRound.PlayerAnsweredNotCorrect(this.ActivePlayer, this.DiscoveredLevel);
      this.ActivePlayer = null;
      if (this.DiscoveredLevel.AllPlayersAnsweredWrong)
      {
        this.RelatedQuestion = this.DiscoveredLevel.Answer;
      }
    }

    private void NoOneWantAnswer()
    {
      this.CurrentGameWall = this.CurrentRound.PlayerAnsweredNotCorrect(this.CurrentRound.FirstPlayer, this.DiscoveredLevel);
      this.CurrentGameWall = this.CurrentRound.PlayerAnsweredNotCorrect(this.CurrentRound.SecondPlayer, this.DiscoveredLevel);
      this.CurrentGameWall = this.CurrentRound.PlayerAnsweredNotCorrect(this.CurrentRound.ThirdPlayer, this.DiscoveredLevel);
      this.CurrentGameWall = this.CurrentRound.PlayerAnsweredNotCorrect(this.CurrentRound.FourthPlayer, this.DiscoveredLevel);
      this.ActivePlayer = null;
      this.RelatedQuestion = this.DiscoveredLevel.Answer;
    }

    private void DiscardLevel()
    {
      this.CurrentGameWall = this.CurrentRound.DiscardLevel(this.DiscoveredLevel);
      this.RelatedQuestion = null;
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
        this.SetDiscoveredLevelCommand.RaiseCanExecuteChanged();
        this.CorrectAnswerCommand.RaiseCanExecuteChanged();
        this.WrongAnswerCommand.RaiseCanExecuteChanged();
        this.NoOneWantAnswerCommand.RaiseCanExecuteChanged();
      }
    }

    public Api.Answer RelatedQuestion
    {
      get { return _relatedQuestion; }
      set { this.Set(ref _relatedQuestion, value); }
    }

    public DelegateCommand<ItemClickEventArgs> SetDiscoveredLevelCommand { get; }

    public ICommand AssetFileLoadedCommand { get; }

    public ICommand AssetImageLoadedCommand { get; }

    public ICommand DiscardLevelCommand { get; }

    public DelegateCommand CloseRelatedQuestionCommand { get; }

    public DelegateCommand CorrectAnswerCommand { get; }

    public DelegateCommand WrongAnswerCommand { get; }

    public DelegateCommand NoOneWantAnswerCommand { get; }

    public ObservableCollection<Api.Player> AllPlayers
    {
      get
      {
        if (CurrentGameWall == null)
        {
          return new ObservableCollection<Player>(Enumerable.Empty<Player>());
        }
        return new ObservableCollection<Player>(new[]
        {
          CurrentGameWall.FirstPlayer,
          CurrentGameWall.SecondPlayer,
          CurrentGameWall.ThirdPlayer,
          CurrentGameWall.FourthPlayer
        });
      }
    }

    public Api.Player ActivePlayer
    {
      get { return _activePlayer; }
      set
      {
        this.Set(ref _activePlayer, value);
        this.CorrectAnswerCommand.RaiseCanExecuteChanged();
        this.WrongAnswerCommand.RaiseCanExecuteChanged();
        this.NoOneWantAnswerCommand.RaiseCanExecuteChanged();
      }
    }

    public void ContinueCurrentRound()
    {
      SetupCurrentRound(_gameApi.CurrentRound);
    }

    public Api.Round StartNextRound(Api.Round round)
    {
      var nextRound = _gameApi.StartNextRound(round);
      if (nextRound != null)
      {
        SetupCurrentRound(nextRound);
      }
      return nextRound;
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