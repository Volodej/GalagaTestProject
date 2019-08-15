using System.Threading.Tasks;
using StateMachines;

namespace LifeCycle.Level.States
{
    public class IncomingWaveState : IAwaitableState<LevelStateType>
    {
        public Task RunState()
        {
            throw new System.NotImplementedException();
        }

        public LevelStateType Type => LevelStateType.IncomingWave;
        public bool IsExitState => false;
    }
}