using System.Threading.Tasks;
using LifeCycle.Game;
using StateMachines;

namespace LifeCycle.Level.States
{
    public class LevelEndState : IAwaitableState<LevelStateType>
    {
        private readonly GameContext _gameContext;

        public LevelEndState(GameContext gameContext)
        {
            _gameContext = gameContext;
        }
        
        public Task RunState()
        {
            _gameContext.LevelNumber++;
            return Task.CompletedTask;
        }

        public LevelStateType Type => LevelStateType.LevelEnd;
        public bool IsExitState => true;
    }
}