using System.Threading.Tasks;
using StateMachines;

namespace LifeCycle.Game.States
{
    public class TopPlayersState : IAwaitableState<GameStateType>
    {
        public Task RunState()
        {
            throw new System.NotImplementedException();
        }

        public GameStateType Type => GameStateType.TopPlayers;
        public bool IsExitState => false;
    }
}