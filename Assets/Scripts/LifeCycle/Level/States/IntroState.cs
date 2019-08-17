using System;
using System.Threading.Tasks;
using LifeCycle.Game;
using StateMachines;
using TopPlayers;
using UIElements;
using UniRx;
using Units;
using Units.Formation;

namespace LifeCycle.Level.States
{
    public class IntroState : IAwaitableState<LevelStateType>
    {
        private readonly GameSoundController _soundController;
        private readonly HudPanel _hudPanel;
        private readonly GameContext _gameContext;
        private readonly ShipsFormation _shipsFormation;
        private readonly TopPlayersStorage _topPlayersStorage;
        private readonly LevelContext _levelContext;
        private readonly PlayerShip _playerShip;

        public IntroState(GameSoundController soundController, HudPanel hudPanel, GameContext gameContext, ShipsFormation shipsFormation,
            TopPlayersStorage topPlayersStorage, LevelContext levelContext, PlayerShip playerShip)
        {
            _soundController = soundController;
            _hudPanel = hudPanel;
            _gameContext = gameContext;
            _shipsFormation = shipsFormation;
            _topPlayersStorage = topPlayersStorage;
            _levelContext = levelContext;
            _playerShip = playerShip;
        }

        public async Task RunState()
        {
            Setup();
            _soundController.PlayNextLevel();
            _hudPanel.SetLevelsCount(_gameContext.LevelNumber + 1);
            _hudPanel.SetPlayerLivesCount(_gameContext.PlayerLives);
            _hudPanel.SetPlayerScore(_gameContext.PlayerScore);
            _hudPanel.SetHighScore(_topPlayersStorage.HighestScore);
            _levelContext.HasWavesToIncome = true;
            await _hudPanel.ShowStageNumber(_gameContext.LevelNumber + 1);
        }

        private void Setup()
        {
            _shipsFormation.PointsForEnemiesStream
                .TakeUntilDestroy(_shipsFormation)
                .Subscribe(points =>
                {
                    var currentScore = _gameContext.AddScore(points);
                    var highestScore = Math.Max(currentScore, _topPlayersStorage.HighestScore);
                    _hudPanel.SetPlayerScore(currentScore);
                    _hudPanel.SetHighScore(highestScore);
                });
            _shipsFormation.LeftEnemiesStream
                .TakeUntilDestroy(_shipsFormation)
                .Where(leftEnemies => leftEnemies == 0 && !_levelContext.HasWavesToIncome)
                .Subscribe(leftEnemies => _levelContext.LevelEndAchieved = true);

            _playerShip.PlayerShipDestroyedStream
                .TakeUntilDestroy(_shipsFormation)
                .Subscribe(_ =>
                {
                    _soundController.PlayFighterDestroyed();
                    _gameContext.IsPlayerAlive = false;
                });
        }

        public LevelStateType Type => LevelStateType.Intro;
        public bool IsExitState => false;
    }
}