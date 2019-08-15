using System.Threading.Tasks;
using DG.Tweening;
using StateMachines;
using TopPlayers;
using UIElements;
using UniRx;
using UnityEngine;

namespace LifeCycle.Game.States
{
    public class RecordApplyingState : IAwaitableState<GameStateType>
    {
        private const float PANEL_POSITION_OFFSCREEN = 300;
        private const float PANEL_MOVEMENT_TIME = 1;
        
        private readonly EnterNamePanel _panel;
        private readonly TopPlayersStorage _scoreStorage;
        private readonly GameContext _gameContext;
        private readonly Vector3 _initialPanelPosition;

        public GameStateType Type => GameStateType.RecordApplying;
        public bool IsExitState => false;

        public RecordApplyingState(EnterNamePanel panel, TopPlayersStorage scoreStorage, GameContext gameContext)
        {
            _panel = panel;
            _scoreStorage = scoreStorage;
            _gameContext = gameContext;
            _initialPanelPosition = panel.transform.localPosition;
        }

        public async Task RunState()
        {
            if (_scoreStorage.LowestScore > _gameContext.PlayerScore)
                return;
            
            _panel.gameObject.SetActive(true);
            await ShowPanel();
            var userName = await _panel.GetUserName();
            _gameContext.SetPlayerPosition(_scoreStorage.AddPlayerScore(userName, _gameContext.PlayerScore));
            await HidePanel();
        }

        private Task ShowPanel()
        {
            var tcs = new TaskCompletionSource<Unit>();
            _panel.transform.position = _initialPanelPosition - new Vector3(0, PANEL_POSITION_OFFSCREEN, 0);
            _panel.transform.DOLocalMove(_initialPanelPosition, PANEL_MOVEMENT_TIME, true)
                .onComplete += () => tcs.SetResult(Unit.Default);
            return tcs.Task;
        }

        private Task HidePanel()
        {
            var tcs = new TaskCompletionSource<Unit>();
            _panel.transform.position = _initialPanelPosition;
            var newPosition = _initialPanelPosition - new Vector3(0, PANEL_POSITION_OFFSCREEN, 0);
            _panel.transform.DOLocalMove(newPosition, PANEL_MOVEMENT_TIME, true)
                .onComplete += () => tcs.SetResult(Unit.Default);
            return tcs.Task;
        }
    }
}