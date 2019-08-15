using System.Threading.Tasks;
using LifeCycle.Game;
using StateMachines;
using Units;
using UnityEngine;

namespace LifeCycle.Level.States
{
    public class WaitingState : IAwaitableState<LevelStateType>
    {
        private readonly GameContext _gameContext;
        private readonly LevelContext _levelContext;
        private readonly PlayerShip _playerShip;

        public WaitingState(GameContext gameContext, LevelContext levelContext, PlayerShip playerShip)
        {
            _gameContext = gameContext;
            _levelContext = levelContext;
            _playerShip = playerShip;
        }
        
        public async Task RunState()
        {
            if (!_gameContext.IsPlayerAlive)
            {
                if (_gameContext.PlayerLives == 0)
                {
                    _levelContext.LevelEndAchieved = true;
                    return;
                }
                else
                {
                    _playerShip.PlaceShipOnScene();
                    _gameContext.TakeOneLife();
                }
            }

            await Task.Delay(Mathf.RoundToInt(_levelContext.TimeToWait * 1000));
        }

        public LevelStateType Type => LevelStateType.Waiting;
        public bool IsExitState => false;
    }
}