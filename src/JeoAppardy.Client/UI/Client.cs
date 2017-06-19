using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using JeoAppardy.Client.Common;
using Reactive.Bindings;

namespace JeoAppardy.Client.UI
{
  public class Client : ViewModel
  {
    private Frame _frame;
    private SerialDevice serialDevice;
    private DataWriter dataWriterObject;
    private DataReader dataReaderObject;
    private ICommand _startGameCommand;
    private bool _isOpen;

    public Client(Frame frame, object parameter)
    {
      _frame = frame;

      this.StartGameCommand = new DelegateCommand(StartGame, () => true);

      var scheduler = UIDispatcherScheduler.Default;
      Ports = new ReactiveCollection<DeviceInformation>(scheduler);

      Task.Run(async () =>
      {
        DeviceInformationCollection ports = await DeviceInformation.FindAllAsync(SerialDevice.GetDeviceSelector());
        ports.ToList<DeviceInformation>().ForEach(p =>
        {
          System.Diagnostics.Debug.WriteLine($"{p.Name}, {p.Id}, {p.Kind}, {p.Properties}");
          Ports.AddOnScheduler(p);
        });
      });

      SelectedPort = new ReactiveProperty<DeviceInformation>(scheduler);
    }

    public ICommand StartGameCommand
    {
      get { return _startGameCommand; }
      set { this.Set(ref _startGameCommand, value); }
    }

    private async void StartGame()
    {
      var deviceInformation = SelectedPort.Value;
      if (deviceInformation != null)
      {
        await Open(deviceInformation);
      }

      var game = await SetupGame();

      _frame.Navigate(typeof(GameWall), game);
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

    public bool IsOpen
    {
      get { return _isOpen; }
      set { this.Set(ref _isOpen, value); }
    }

    public ReactiveCollection<DeviceInformation> Ports { get; }

    public ReactiveProperty<DeviceInformation> SelectedPort { get; }

    public async Task<bool> Open(DeviceInformation portName, uint baudRate = 9600,
      SerialParity parity = SerialParity.None, ushort dataBits = 8,
      SerialStopBitCount stopBits = SerialStopBitCount.One)
    {
      // Close open port
      Close();

      // Get a list of devices that match the given name
      DeviceInformation deviceInfo = await DeviceInformation.CreateFromIdAsync(portName.Id);

      // If any device found...
      if (deviceInfo != null)
      {
        // Create a serial port device from the COM port device ID
        this.serialDevice = await SerialDevice.FromIdAsync(deviceInfo.Id);

        // If serial device is valid...
        if (this.serialDevice != null)
        {
          // Setup serial port configuration
          this.serialDevice.StopBits = stopBits;
          this.serialDevice.Parity = parity;
          this.serialDevice.BaudRate = baudRate;
          this.serialDevice.DataBits = dataBits;

          // Create a single device writer for this port connection
          this.dataWriterObject = new DataWriter(this.serialDevice.OutputStream);

          // Create a single device reader for this port connection
          this.dataReaderObject = new DataReader(this.serialDevice.InputStream);

          // Allow partial reads of the input stream
          this.dataReaderObject.InputStreamOptions = InputStreamOptions.Partial;

          // Port is now open
          this.IsOpen = true;
        }
      }

      return this.IsOpen;
    }

    public void Close()
    {
      // If serial device defined...
      if (this.serialDevice != null)
      {
        // Dispose and clear device
        this.serialDevice.Dispose();
        this.serialDevice = null;
      }

      // If data reader defined...
      if (this.dataReaderObject != null)
      {
        // Detatch reader stream
        this.dataReaderObject.DetachStream();

        // Dispose and clear data reader
        this.dataReaderObject.Dispose();
        this.dataReaderObject = null;
      }

      // If data writer defined...
      if (this.dataWriterObject != null)
      {
        // Detatch writer stream
        this.dataWriterObject.DetachStream();

        // Dispose and clear data writer
        this.dataWriterObject.Dispose();
        this.dataWriterObject = null;
      }

      // Port now closed
      this.IsOpen = false;
    }
  }
}