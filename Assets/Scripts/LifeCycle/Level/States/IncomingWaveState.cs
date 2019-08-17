using System.Threading.Tasks;
using StateMachines;
using Units.Formation;

namespace LifeCycle.Level.States
{
    public class IncomingWaveState : IAwaitableState<LevelStateType>
    {
        private readonly ShipsFormation _shipsFormation;
        private readonly LevelContext _levelContext;

        public IncomingWaveState(ShipsFormation shipsFormation, LevelContext levelContext)
        {
            _shipsFormation = shipsFormation;
            _levelContext = levelContext;
        }

        public async Task RunState()
        {
            _levelContext.HasWavesToIncome = _shipsFormation.LeftIncomingWaves - 1 > 0;
            await _shipsFormation.SpawnEnemiesWave();
        }

        public LevelStateType Type => LevelStateType.IncomingWave;
        public bool IsExitState => false;
    }
}