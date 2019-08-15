using System.Threading.Tasks;
using StateMachines;

namespace LifeCycle.Game.States
{
    public class LevelSelectionState : IAwaitableState<GameStateType>
    {
        public Task RunState()
        {
            throw new System.NotImplementedException();
        }

        public GameStateType Type => GameStateType.LevelSelection;
        public bool IsExitState => false;
    }
}