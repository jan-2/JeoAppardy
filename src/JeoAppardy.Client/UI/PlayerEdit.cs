using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using JeoAppardy.Client.Common;

namespace JeoAppardy.Client.UI
{
  public class PlayerEdit : ViewModel
  {
    private Frame _frame;
    private DelegateCommand _closeCommand;
    private string _playerOne;
    private string _playerTwo;
    private string _playerThree;
    private string _playerFour;

    public PlayerEdit(Frame frame, Game game)
    {
      this._frame = frame;
      this.Game = game;

      this.CloseCommand = new DelegateCommand(
        () =>
        {
          this.Game.SetupPlayerOne(new Name(PlayerOne));
          this.Game.SetupPlayerTwo(new Name(PlayerTwo));
          this.Game.SetupPlayerThree(new Name(PlayerThree));
          this.Game.SetupPlayerFour(new Name(PlayerFour));
          this._frame.Navigate(typeof(GameWall), this.Game);
        },
        () => !string.IsNullOrEmpty(PlayerOne) && !string.IsNullOrEmpty(PlayerTwo) && !string.IsNullOrEmpty(PlayerThree) && !string.IsNullOrEmpty(PlayerFour));

      this.PlayerOne = this.Game.CurrentRound.FirstPlayer.Name ?? "Spieler 1";
      this.PlayerTwo = this.Game.CurrentRound.SecondPlayer.Name ?? "Spieler 2";
      this.PlayerThree = this.Game.CurrentRound.ThirdPlayer.Name ?? "Spieler 3";
      this.PlayerFour = this.Game.CurrentRound.FourthPlayer.Name ?? "Spieler 4";
    }

    public Game Game { get; set; }

    public DelegateCommand CloseCommand
    {
      get { return _closeCommand; }
      private set { this.Set(ref _closeCommand, value); }
    }

    public string PlayerOne
    {
      get { return _playerOne; }
      set
      {
        this.Set(ref _playerOne, value);
        CloseCommand.RaiseCanExecuteChanged();
      }
    }

    public string PlayerTwo
    {
      get { return _playerTwo; }
      set
      {
        this.Set(ref _playerTwo, value);
        CloseCommand.RaiseCanExecuteChanged();
      }
    }

    public string PlayerThree
    {
      get { return _playerThree; }
      set
      {
        this.Set(ref _playerThree, value);
        CloseCommand.RaiseCanExecuteChanged();
      }
    }

    public string PlayerFour
    {
      get { return _playerFour; }
      set
      {
        this.Set(ref _playerFour, value);
        CloseCommand.RaiseCanExecuteChanged();
      }
    }
  }
}