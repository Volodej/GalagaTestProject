using LifeCycle.Level.States;
using StateMachines;

namespace LifeCycle.Level
{
    public class LevelLifeCycleTransitionManager : StateTransitionManager<LevelStateType, LevelContext>
    {
        public LevelLifeCycleTransitionManager(IntroState introState, IncomingWaveState incomingWaveState, WaitingState waitingState,
            DivingWaveState divingWaveState, LevelEndState levelEndState)
        {
            AddState(introState)
                .AllowTransition(waitingState, _ => true)
                .Build();
            
            AddState(incomingWaveState)
                .AllowTransition(waitingState, _ => true)
                .Build();
            
            AddState(divingWaveState)
                .AllowTransition(waitingState, _ => true)
                .Build();
            
            AddState(waitingState)
                .AllowTransition(levelEndState, context => context.LevelEndAchieved)
                .AllowTransition(incomingWaveState, context => context.HasWavesToIncome)
                .AllowTransition(divingWaveState, context => true)
                .Build();
        }
    }
}