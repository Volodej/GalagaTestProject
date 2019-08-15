using System;
using System.Threading.Tasks;
using LifeCycle.Game;
using StateMachines;
using UIElements;

namespace LifeCycle.Level.States
{
    public class IntroState : IAwaitableState<LevelStateType>
    {
        private readonly GameSoundController _soundController;
        private readonly HudPanel _hudPanel;
        private readonly GameContext _gameContext;

        public IntroState(GameSoundController soundController, HudPanel hudPanel, GameContext gameContext)
        {
            _soundController = soundController;
            _hudPanel = hudPanel;
            _gameContext = gameContext;
        }
        
        public async Task RunState()
        {
            _soundController.PlayNextLevel();
            await _hudPanel.ShowStageNumber(_gameContext.LevelNumber);
        }

        public LevelStateType Type => LevelStateType.Intro;
        public bool IsExitState => false;
    }
}