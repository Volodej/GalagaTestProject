using StateMachines;

namespace LifeCycle.Level
{
    public class LevelLifeCycleStateMachine : WaitingStateMachine<LevelStateType, LevelContext>
    {
        public LevelLifeCycleStateMachine(LevelLifeCycleTransitionManager transitionManager, LevelContext context)
            : base(transitionManager, context)
        {
        }
    }
}