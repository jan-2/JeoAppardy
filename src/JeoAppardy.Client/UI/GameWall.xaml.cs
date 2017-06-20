﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace JeoAppardy.Client.UI
{
  public sealed partial class GameWall : Page
  {
    public GameWall()
    {
      this.DataContext = this;
      this.InitializeComponent();
    }

    public Game ViewModel { get; private set; }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      this.ViewModel = e.Parameter as Game;
      if (this.ViewModel == null)
      {
        var gameApi = e.Parameter as Api.Game;
        this.ViewModel = new Game(gameApi);
        this.ViewModel.StartFirstRound();
      }
      this.Loaded += (sender, args) =>
      {
        if (!this.ViewModel.CurrentGameWall.AllPlayersSet)
        {
          // navigate to Player Eingabe
          this.Frame.Navigate(typeof(PlayerEditView), this.ViewModel);
        }
        else
        {
          this.ViewModel.ContinueCurrentRound();
        }
      };
      base.OnNavigatedTo(e);
    }
  }
}