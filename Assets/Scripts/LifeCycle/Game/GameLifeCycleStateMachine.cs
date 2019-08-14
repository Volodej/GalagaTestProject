using StateMachines;

namespace LifeCycle.Game
{
    public class GameLifeCycleStateMachine : WaitingStateMachine<GameStateType, GameContext>
    {
        public GameLifeCycleStateMachine(GameContext gameContext)
        {
            
        }
    }
}