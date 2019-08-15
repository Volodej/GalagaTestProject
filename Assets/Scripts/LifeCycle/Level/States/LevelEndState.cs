using System.Threading.Tasks;
using StateMachines;

namespace LifeCycle.Level.States
{
    public class LevelEndState : IAwaitableState<LevelStateType>
    {
        public Task RunState()
        {
            throw new System.NotImplementedException();
        }

        public LevelStateType Type => LevelStateType.LevelEnd;
        public bool IsExitState => true;
    }
}