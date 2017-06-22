﻿using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace JeoAppardy.Client.UI
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class WinnerView : Page
  {
    public WinnerView()
    {
      this.InitializeComponent();
    }

    public Winner ViewModel { get; private set; }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      this.ViewModel = new Winner(this.Frame, e.Parameter as Api.Game);
    }
  }
}