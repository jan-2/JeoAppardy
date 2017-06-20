using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;
using Newtonsoft.Json;

namespace JeoAppardy.Client.Buzzer
{
  public class Hardware
  {
    public class Result
    {
      public StateEnum status { get; set; }

      /// <summary>
      /// Die Nummer des Spielers der als erstes gedrückt hat (1 - 4).
      /// </summary>
      public int sieger { get; set; }

      /// <summary>
      /// Aktueller Status.
      /// </summary>
      /// <param name="state"></param>
      public Result(StateEnum state)
      {
        status = state;
      }

      public override string ToString()
      {
        return $"{status} - Sieger: {sieger}";
      }
    }

    private enum CommandEnum
    {
      Reset,
      State
    }

    private SerialDevice _serial_device;
    private DataWriter _data_writer_object;
    private DataReader _data_reader_object;
    private CancellationTokenSource _cancellation_token_source;

    private bool _is_locked;

    private static readonly object InstanceLock = new object();
    private static readonly object SendLockObject = new object();

    private static Hardware _instance;

    public bool IsOpen { get; set; }

    private Hardware()
    {
    }

    public static Hardware GetInstance()
    {
      lock (InstanceLock)
      {
        return _instance ?? (_instance = new Hardware());
      }
    }

    public async Task<bool> Open(DeviceInformation port_name)
    {
      Close();

      DeviceInformation device_info = await DeviceInformation.CreateFromIdAsync(port_name.Id);

      if (device_info == null) return false;

      _serial_device = await SerialDevice.FromIdAsync(device_info.Id);

      if (_serial_device == null) return false;

      _serial_device.StopBits = SerialStopBitCount.One;
      _serial_device.Parity = SerialParity.None;
      _serial_device.BaudRate = 9600;
      _serial_device.DataBits = 8;
      _serial_device.Handshake = SerialHandshake.XOnXOff;
      _serial_device.ReadTimeout = TimeSpan.FromSeconds(1);
      _serial_device.WriteTimeout = TimeSpan.FromSeconds(1);

      _data_writer_object = new DataWriter(_serial_device.OutputStream);
      _data_reader_object =
        new DataReader(_serial_device.InputStream) {InputStreamOptions = InputStreamOptions.Partial};

      _cancellation_token_source = new CancellationTokenSource();

      IsOpen = true;

      return IsOpen;
    }

    public void Close()
    {
      if (_cancellation_token_source != null)
      {
        _cancellation_token_source.Cancel();
        _cancellation_token_source = null;
      }

      if (_serial_device != null)
      {
        _serial_device.Dispose();
        _serial_device = null;
      }

      if (_data_reader_object != null)
      {
        _data_reader_object.DetachStream();

        _data_reader_object.Dispose();
        _data_reader_object = null;
      }

      if (_data_writer_object != null)
      {
        _data_writer_object.DetachStream();

        _data_writer_object.Dispose();
        _data_writer_object = null;
      }

      IsOpen = false;
    }

    public async Task<Result> Reset()
    {
      return await SendCommandAndReceiveState(CommandEnum.Reset);
    }

    public async Task<Result> GetCurrentState()
    {
      return await SendCommandAndReceiveState(CommandEnum.State);
    }

    private async Task<string> ReadAsync(CancellationToken cancellation_token)
    {
      uint ReadBufferLength = 1024;
      cancellation_token.ThrowIfCancellationRequested();
      _data_reader_object.InputStreamOptions = InputStreamOptions.Partial;
      var load_async_task = _data_reader_object.LoadAsync(ReadBufferLength).AsTask(cancellation_token);
      UInt32 bytes_read = await load_async_task;
      if (bytes_read > 0)
      {
        return _data_reader_object.ReadString(bytes_read);
      }

      return string.Empty;
    }

    private async Task<Result> SendCommandAndReceiveState(CommandEnum command)
    {
      lock (SendLockObject)
      {
        if (_is_locked)
        {
          return new Result(StateEnum.Undefined);
        }

        _is_locked = true;
      }

      try
      {
        if (!IsOpen)
        {
          return new Result(StateEnum.Undefined);
        }

        string command_as_string;

        switch (command)
        {
          case CommandEnum.Reset:
            command_as_string = "Reset";
            break;
          case CommandEnum.State:
            command_as_string = "State";
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof(command), command, null);
        }

        _data_writer_object.WriteString(command_as_string + "\n");
        await _data_writer_object.StoreAsync();

        var result = await ReadAsync(_cancellation_token_source.Token);
        return JsonConvert.DeserializeObject<Result>(result);
      }
      finally
      {
        _is_locked = false;
      }
    }
  }
}