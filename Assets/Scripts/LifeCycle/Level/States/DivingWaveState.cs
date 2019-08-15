using System.Threading.Tasks;
using StateMachines;

namespace LifeCycle.Level.States
{
    public class DivingWaveState : IAwaitableState<LevelStateType>
    {
        public Task RunState()
        {
            throw new System.NotImplementedException();
        }

        public LevelStateType Type => LevelStateType.DivingWave;
        public bool IsExitState => false;
    }
}