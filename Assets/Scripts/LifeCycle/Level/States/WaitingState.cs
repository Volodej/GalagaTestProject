using System;
using System.Threading.Tasks;
using LifeCycle.Game;
using StateMachines;
using UIElements;
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
        private readonly HudPanel _hudPanel;

        public WaitingState(GameContext gameContext, LevelContext levelContext, PlayerShip playerShip, ShipsFormation shipsFormation,
            HudPanel hudPanel)
        {
            _gameContext = gameContext;
            _levelContext = levelContext;
            _playerShip = playerShip;
            _shipsFormation = shipsFormation;
            _hudPanel = hudPanel;
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
                    _hudPanel.SetPlayerLivesCount(_gameContext.PlayerLives);
                }
            }

            if (!_levelContext.LevelEndAchieved)
                await Task.Delay(TimeSpan.FromSeconds(_levelContext.TimeToWait));
        }

        public LevelStateType Type => LevelStateType.Waiting;
        public bool IsExitState => false;
    }
}