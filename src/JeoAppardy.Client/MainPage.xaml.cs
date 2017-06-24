using System;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using JeoAppardy.Client.UI;

namespace JeoAppardy.Client
{
  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      DataContext = this;
      this.InitializeComponent();
    }

    public UI.Client ViewModel { get; private set; }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      this.ViewModel = new UI.Client(this.Frame, e.Parameter);
      base.OnNavigatedTo(e);
    }

    private async void ButtonSettingsOnTapped(object sender, TappedRoutedEventArgs e)
    {
      FolderPicker folderPicker = new FolderPicker();
      folderPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
      folderPicker.FileTypeFilter.Add("*");
      var folder = await folderPicker.PickSingleFolderAsync();
      if (folder != null)
      {
        // Application now has read/write access to all contents in the picked folder (including other sub-folder contents)
        StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
        ViewModel.Settings.AssetsRoundPath = folder.Path;
      }
    }
  }
}