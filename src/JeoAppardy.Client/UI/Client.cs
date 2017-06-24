﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using JeoAppardy.Client.Buzzer;
using JeoAppardy.Client.Common;
using Reactive.Bindings;

namespace JeoAppardy.Client.UI
{
  public class Client : ViewModel
  {
    private Frame _frame;
    private ICommand _startGameCommand;
    private ClientSettings _settings;

    public Client(Frame frame)
    {
      _frame = frame;

      this.Settings = ClientSettings.Instance;

      this.StartGameCommand = new DelegateCommand(StartGame, () => true);

      var scheduler = UIDispatcherScheduler.Default;
      Ports = new ReactiveCollection<DeviceInformation>(scheduler);
      SelectedPort = new ReactiveProperty<DeviceInformation>(scheduler);

      Task.Run(SetupHardware);
    }

    private async Task SetupHardware()
    {
      DeviceInformationCollection ports = await DeviceInformation.FindAllAsync(SerialDevice.GetDeviceSelector());
      ports.ToList()
        .ForEach(p =>
        {
          Debug.WriteLine($"{p.Name}, {p.Id}, {p.Kind}, {p.Properties}");
          Ports.AddOnScheduler(p);
        });

      SelectedPort.Value = ports.SingleOrDefault(p => p.Name == "Jeopardy");
    }

    public ClientSettings Settings
    {
      get { return _settings; }
      private set { this.Set(ref _settings, value); }
    }

    public ReactiveCollection<DeviceInformation> Ports { get; }

    public ReactiveProperty<DeviceInformation> SelectedPort { get; }

    public ICommand StartGameCommand
    {
      get { return _startGameCommand; }
      private set { this.Set(ref _startGameCommand, value); }
    }

    private async void StartGame()
    {
      var deviceInformation = SelectedPort.Value;
      if (deviceInformation != null)
      {
        // Soll das Spiel auch ohne Verbindung starten?
        // Ja, das Spiel sollte auch per Hand steuerbar sein
        try
        {
          await Hardware.GetInstance().Open(deviceInformation);
        }
        catch (Exception ex)
        {
          // Bei TimeOut evtl. noch mal wiederholen.
          Debug.WriteLine(ex);
          Debugger.Break();
          throw;
        }
      }

      var game = await SetupGame();

      _frame.Navigate(typeof(GameWall), game);
    }

    public static async Task<Api.Game> SetupGame()
    {
      // Laden der Boards vom Dateipfad
      var installedLocation = ClientSettings.Instance.AssetsRoundPath;

      var firstBoardDefinition = await GetDefinitionOf($"{installedLocation}\\round_1\\boardOne.json");
      var secondBoardDefinition = await GetDefinitionOf($"{installedLocation}\\round_2\\boardTwo.json");
      var thirdBoardDefinition = await GetDefinitionOf($"{installedLocation}\\round_3\\boardThree.json");
      var fourthBoardDefinition = await GetDefinitionOf($"{installedLocation}\\round_4\\boardFour.json");
      var finalBoardDefinition = await GetDefinitionOf($"{installedLocation}\\round_final\\boardFinal.json");

      var boardOne = Api.Board.FromJson(firstBoardDefinition);
      var boardTwo = Api.Board.FromJson(secondBoardDefinition);
      var boardThree = Api.Board.FromJson(thirdBoardDefinition);
      var boardFour = Api.Board.FromJson(fourthBoardDefinition);
      var boardFinal = Api.Board.FromJson(finalBoardDefinition);

      return Api.Game.SetupWithBoards(boardOne, boardTwo, boardThree, boardFour, boardFinal);
    }

    private static async Task<string> GetDefinitionOf(string assetFilePath)
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