using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using JeoAppardy.Client.Api;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace JeoAppardy.Client.UI
{
  public sealed partial class GameWall : Page
  {
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
  }
}