using System;
using System.Threading.Tasks;
using Units;
using UnityEngine;
using Zenject;

namespace LifeCycle.Game
{
    public class GameStarter : MonoBehaviour
    {
        private GameLifeCycleStateMachine _gameStateMachine;
        private PlayerShip _playerShip;
        private GameContext _gameContext;

        [Inject]
        public void Initialize(GameLifeCycleStateMachine gameStateMachine, PlayerShip playerShip, GameContext gameContext)
        {
            Debug.Log("Initialize GameManager");
            _gameStateMachine = gameStateMachine;
            _playerShip = playerShip;
            _gameContext = gameContext;
        }

        private async void Start()
        {
            Application.targetFrameRate = 60;
            Debug.Log("Start GameManager");
            _playerShip.PlaceShipOnScene();
            _gameContext.IsPlayerAlive = true;
            _gameContext.Reset();
            await _gameStateMachine.StartWithState(GameStateType.Level);
        }
    }
}