using System;
using System.Threading.Tasks;
using LifeCycle.Game;
using StateMachines;
using Units;
using Units.Formation;
using UnityEngine;

namespace LifeCycle.Level.States
{
    public class WaitingState : IAwaitableState<LevelStateType>
    {
        private readonly GameContext _gameContext;
        private readonly LevelContext _levelContext;
        private readonly PlayerShip _playerShip;
        private readonly ShipsFormation _shipsFormation;

        public WaitingState(GameContext gameContext, LevelContext levelContext, PlayerShip playerShip, ShipsFormation shipsFormation)
        {
            _gameContext = gameContext;
            _levelContext = levelContext;
            _playerShip = playerShip;
            _shipsFormation = shipsFormation;
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

            if (!_levelContext.LevelEndAchieved)
                await Task.Delay(TimeSpan.FromSeconds(_levelContext.TimeToWait));
        }

        public LevelStateType Type => LevelStateType.Waiting;
        public bool IsExitState => false;
    }
}