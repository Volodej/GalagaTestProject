using System.Threading.Tasks;
using StateMachines;
using Units.Formation;

namespace LifeCycle.Level.States
{
    public class DivingWaveState : IAwaitableState<LevelStateType>
    {
        private readonly ShipsFormation _shipsFormation;
        private readonly LevelContext _levelContext;
        private readonly LevelSettings _levelSettings;

        public DivingWaveState(ShipsFormation shipsFormation, LevelContext levelContext, LevelSettings levelSettings)
        {
            _shipsFormation = shipsFormation;
            _levelContext = levelContext;
            _levelSettings = levelSettings;
        }
        
        public async Task RunState()
        {
            await _shipsFormation.StartAttackWave();
            _levelContext.TimeToWait = _levelSettings.TimeBetweenWaves;
        }

        public LevelStateType Type => LevelStateType.DivingWave;
        public bool IsExitState => false;
    }
}