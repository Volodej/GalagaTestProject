using StateMachines;

namespace LifeCycle.Game
{
    public class GameLifeCycleStateMachine : WaitingStateMachine<GameStateType, GameContext>
    {
        public GameLifeCycleStateMachine(GameLifeCycleTransitionManager transitionManager, GameContext gameContext)
            : base(transitionManager, gameContext)
        {
            
        }
    }
}