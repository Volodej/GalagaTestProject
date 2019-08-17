using System.Threading.Tasks;
using DG.Tweening;
using StateMachines;
using TopPlayers;
using UIElements;
using UniRx;
using Units;
using UnityEngine;

namespace LifeCycle.Game.States
{
    public class TopPlayersState : IAwaitableState<GameStateType>
    {
        private const float PANEL_MOVEMENT_TIME = 1;

        private readonly TopScorePanel _scorePanel;
        private readonly GameContext _gameContext;
        private readonly TopPlayersStorage _topPlayersStorage;
        private readonly PlayerShip _playerShip;
        private readonly Vector3 _initialPanelPosition;
        private int _panelPositionOffscreen;

        public TopPlayersState(TopScorePanel scorePanel, GameContext gameContext, TopPlayersStorage topPlayersStorage,
            PlayerShip playerShip)
        {
            _scorePanel = scorePanel;
            _gameContext = gameContext;
            _topPlayersStorage = topPlayersStorage;
            _playerShip = playerShip;
            _initialPanelPosition = scorePanel.transform.localPosition;
            _panelPositionOffscreen = Screen.width;
        }

        public async Task RunState()
        {
            _scorePanel.SetupPanel(_topPlayersStorage.Top10Players, _gameContext.PlayerPositionInRating);
            await ShowPanel();
            await _scorePanel.WaitForNewGame();
            await HidePanel();
            _gameContext.Reset();
            _playerShip.PlaceShipOnScene();
            _gameContext.IsPlayerAlive = true;
            _gameContext.LevelNumber--;
        }

        public GameStateType Type => GameStateType.TopPlayers;
        public bool IsExitState => false;

        private Task ShowPanel()
        {
            var tcs = new TaskCompletionSource<Unit>();
            _scorePanel.transform.localPosition = _initialPanelPosition - new Vector3(_panelPositionOffscreen, 0, 0);
            _scorePanel.GetComponent<RectTransform>().DOAnchorPosX(0, PANEL_MOVEMENT_TIME, true)
                .onComplete += () => tcs.SetResult(Unit.Default);
            return tcs.Task;
        }

        private Task HidePanel()
        {
            var tcs = new TaskCompletionSource<Unit>();
            _scorePanel.transform.localPosition = _initialPanelPosition;
            _scorePanel.GetComponent<RectTransform>().DOAnchorPosX(-_panelPositionOffscreen, PANEL_MOVEMENT_TIME, true)
                .onComplete += () => tcs.SetResult(Unit.Default);
            return tcs.Task;
        }
    }
}