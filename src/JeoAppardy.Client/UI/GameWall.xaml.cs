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
      this.ViewModel = new Game(e.Parameter as Api.Game);
      this.ViewModel.StartFirstRound();
      this.Loaded += (sender, args) =>
      {
        if (!this.ViewModel.CurrentGameWall.AllPlayersSet)
        {
          // navigate to Player Eingabe
          this.Frame.Navigate(typeof(PlayerEditView), this.ViewModel.CurrentRound);
        }
      };
      base.OnNavigatedTo(e);
    }
  }
}