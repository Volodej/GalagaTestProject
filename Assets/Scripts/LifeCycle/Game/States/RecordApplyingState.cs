using System.Threading.Tasks;
using DG.Tweening;
using StateMachines;
using TopPlayers;
using UIElements;
using UniRx;
using UnityEngine;
using UserInput;

namespace LifeCycle.Game.States
{
    public class RecordApplyingState : IAwaitableState<GameStateType>
    {
        private const float PANEL_MOVEMENT_TIME = 1;

        private readonly EnterNamePanel _panel;
        private readonly TopPlayersStorage _scoreStorage;
        private readonly GameContext _gameContext;
        private readonly IUserInput _userInput;
        private readonly GameSoundController _soundController;
        private readonly Vector3 _initialPanelPosition;
        private readonly int _panelPositionOffscreen;

        public GameStateType Type => GameStateType.RecordApplying;
        public bool IsExitState => false;

        public RecordApplyingState(EnterNamePanel panel, TopPlayersStorage scoreStorage, GameContext gameContext, IUserInput userInput,
            GameSoundController soundController)
        {
            _panel = panel;
            _scoreStorage = scoreStorage;
            _gameContext = gameContext;
            _userInput = userInput;
            _soundController = soundController;
            _initialPanelPosition = panel.transform.localPosition;
            _panelPositionOffscreen = Screen.height / 2;
        }

        public async Task RunState()
        {
            _userInput.Disable();

            if (_scoreStorage.LowestScore > _gameContext.PlayerScore)
            {
                _soundController.PlayNoRecord();
                return;
            }
            
            _soundController.PlayNewRecord();

            _panel.gameObject.SetActive(true);
            _panel.SetupPanel(_gameContext.PlayerScore);
            await ShowPanel();
            var userName = await _panel.GetUserName();
            _gameContext.SetPlayerPositionInRating(_scoreStorage.AddPlayerScore(userName, _gameContext.PlayerScore));
            await HidePanel();
        }

        private Task ShowPanel()
        {
            var tcs = new TaskCompletionSource<Unit>();
            _panel.transform.localPosition = _initialPanelPosition - new Vector3(0, _panelPositionOffscreen, 0);
            _panel.GetComponent<RectTransform>().DOAnchorPosY(0, PANEL_MOVEMENT_TIME, true)
                .onComplete += () => tcs.SetResult(Unit.Default);
            return tcs.Task;
        }

        private Task HidePanel()
        {
            var tcs = new TaskCompletionSource<Unit>();
            _panel.transform.localPosition = _initialPanelPosition;
            _panel.GetComponent<RectTransform>().DOAnchorPosY(-_panelPositionOffscreen, PANEL_MOVEMENT_TIME, true)
                .onComplete += () => tcs.SetResult(Unit.Default);
            return tcs.Task;
        }
    }
}