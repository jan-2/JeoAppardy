using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Core;
using JeoAppardy.Client.Common;

namespace JeoAppardy.Client.UI
{
  public class ClientSettings : Notifyable
  {
    private static ClientSettings _instance;
    private string _assetsRoundPath;

    readonly ApplicationDataContainer settingsContainer = ApplicationData.Current.RoamingSettings;

    public ClientSettings()
    {
      Task.Run(async () =>
      {
        StorageFolder installedLocation = await Package.Current.InstalledLocation.GetFolderAsync("Assets");
        var path = settingsContainer.Values["AssetsRoundPath"] as string;
        StorageFolder folder = (!string.IsNullOrEmpty(path) ? await StorageFolder.GetFolderFromPathAsync(path) : null) ?? installedLocation;
        await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
          () =>
          {
            // Your UI update code goes here!
            if (folder != null)
            {
              StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
              this.AssetsRoundPath = folder.Path;
            }
          }
        );
      });
    }

    public void SaveSettings()
    {
      settingsContainer.Values["AssetsRoundPath"] = this.AssetsRoundPath;
    }

    public static ClientSettings Instance
    {
      get { return _instance ?? (_instance = new ClientSettings()); }
    }

    public string AssetsRoundPath
    {
      get { return _assetsRoundPath; }
      set { this.Set(ref _assetsRoundPath, value); }
    }
  }
}