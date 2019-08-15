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

        [Inject]
        public void Initialize(GameLifeCycleStateMachine gameStateMachine, PlayerShip playerShip)
        {
            Debug.Log("Initialize GameManager");
            _gameStateMachine = gameStateMachine;
            _playerShip = playerShip;
        }

        private async void Start()
        {
            Debug.Log("Start GameManager");
            _playerShip.PlaceShipOnScene();
            await _gameStateMachine.StartWithState(GameStateType.Level);
        }
    }
}