using System.Threading.Tasks;
using LifeCycle.Game;
using StateMachines;

namespace LifeCycle.Level.States
{
    public class LevelEndState : IAwaitableState<LevelStateType>
    {
        public LevelEndState()
        {
        }
        
        public Task RunState()
        {
            return Task.CompletedTask;
        }

        public LevelStateType Type => LevelStateType.LevelEnd;
        public bool IsExitState => true;
    }
}