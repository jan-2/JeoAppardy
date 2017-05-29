using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeoAppardy.Client.UI
{
  class Game : ViewModel
  {
    private Api.Game _gameApi;
    private Api.Round _currentRoundApi;
    private Api.GameWall _currentGameWall;

    public Game(Api.Game gameApi)
    {
      _gameApi = gameApi;
    }

    public Api.Round CurrentRound
    {
      get { return _currentRoundApi;  }
      private set { this.Set(ref _currentRoundApi, value); }
    }
    public Api.GameWall CurrentGameWall
    {
      get { return _currentGameWall; }
      set { this.Set(ref _currentGameWall, value); }
    }

    public void StartFirstRound()
    {
      SetupCurrentRound(_gameApi.StartFirstRound());
    }
    public void StartSecondRound()
    {
      SetupCurrentRound(_gameApi.StartSecondRound());
    }
    public void StartThirdRound()
    {
      SetupCurrentRound(_gameApi.StartThirdRound());
    }
    public void StartFourthRound()
    {
      SetupCurrentRound(_gameApi.StartFourthRound());
    }
    public void StartFinalRound()
    {
      SetupCurrentRound(_gameApi.StartFinalRound());
    }

    private void SetupCurrentRound(Api.Round currentRound)
    {
      CurrentRound = currentRound;
      CurrentGameWall = CurrentRound.GameWall;
    }

    public void SetupPlayerOne(Name name)
    {
      CurrentGameWall = CurrentRound.SetFirstPlayerName(name.Value);
    }
    public void SetupPlayerTwo(Name name)
    {
      CurrentGameWall = CurrentRound.SetSecondPlayerName(name.Value);
    }
    public void SetupPlayerThree(Name name)
    {
      CurrentGameWall = CurrentRound.SetThirdPlayerName(name.Value);
    }
    public void SetupPlayerFour(Name name)
    {
      CurrentGameWall = CurrentRound.SetFourthPlayerName(name.Value);
    }

  }
}
