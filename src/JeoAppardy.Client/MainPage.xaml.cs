using System;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using System.Text;

namespace JeoAppardy.Client
{
  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();
    }

    private async void startGame_Click(object sender, RoutedEventArgs e)
    {
      var game = await SetupGame();

      this.Frame.Navigate(typeof(UI.GameWall), game);
    }

    async Task<Api.Game> SetupGame()
    {
      // Laden der Boards vom Dateipfad
      var installedLocation = Package.Current.InstalledLocation;

      var firstBoardDefinition = await GetDefinitionOf($"{installedLocation.Path}\\Assets\\boardOne.json");
      var secondBoardDefinition = await GetDefinitionOf($"{installedLocation.Path}\\Assets\\boardTwo.json");
      var thirdBoardDefinition = await GetDefinitionOf($"{installedLocation.Path}\\Assets\\boardThree.json");
      var fourthBoardDefinition = await GetDefinitionOf($"{installedLocation.Path}\\Assets\\boardFour.json");
      var finalBoardDefinition = await GetDefinitionOf($"{installedLocation.Path}\\Assets\\boardFinal.json");

      var boardOne = Api.Board.FromJson(firstBoardDefinition);
      var boardTwo = Api.Board.FromJson(secondBoardDefinition);
      var boardThree = Api.Board.FromJson(thirdBoardDefinition);
      var boardFour = Api.Board.FromJson(fourthBoardDefinition);
      var boardFinal = Api.Board.FromJson(finalBoardDefinition);

      return Api.Game.SetupWithBoards(boardOne, boardTwo, boardThree, boardFour, boardFinal);
    }

    async Task<string> GetDefinitionOf(string assetFilePath)
    {
      var boardDefinitionFile = await StorageFile.GetFileFromPathAsync(assetFilePath);

      var stream = await boardDefinitionFile.OpenSequentialReadAsync();

      using (StreamReader reader = new StreamReader(stream.AsStreamForRead(), Encoding.UTF8))
      {
        return await reader.ReadToEndAsync();
      }
    }
  }
}