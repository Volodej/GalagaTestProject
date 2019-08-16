using System.Threading.Tasks;
using StateMachines;
using Units.Formation;

namespace LifeCycle.Level.States
{
    public class IncomingWaveState : IAwaitableState<LevelStateType>
    {
        private readonly ShipsFormation _shipsFormation;
        private readonly LevelContext _levelContext;
        private readonly LevelSettings _levelSettings;

        public IncomingWaveState(ShipsFormation shipsFormation, LevelContext levelContext, LevelSettings levelSettings)
        {
            _shipsFormation = shipsFormation;
            _levelContext = levelContext;
            _levelSettings = levelSettings;
        }

        public async Task RunState()
        {
            _levelContext.HasWavesToIncome = _shipsFormation.LeftIncomingWaves - 1 > 0;
            await _shipsFormation.SpawnEnemiesWave();
            _levelContext.TimeToWait = _levelSettings.TimeBetweenWaves;
        }

        public LevelStateType Type => LevelStateType.IncomingWave;
        public bool IsExitState => false;
    }
}