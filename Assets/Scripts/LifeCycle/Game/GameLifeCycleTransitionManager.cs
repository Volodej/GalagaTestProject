using LifeCycle.Game.States;
using StateMachines;

namespace LifeCycle.Game
{
    public sealed class GameLifeCycleTransitionManager : StateTransitionManager<GameStateType, GameContext>
    {
        public GameLifeCycleTransitionManager(LevelState levelState, LevelSelectionState levelSelectionState,
            RecordApplyingState recordApplyingState, TopPlayersState topPlayersState)
        {
            AddState(levelState)
                .AllowTransition(levelSelectionState, context => context.IsPlayerAlive)
                .AllowTransition(recordApplyingState, _ => true)
                .Build();
            
            AddState(levelSelectionState)
                .AllowTransition(levelState, context => context.NewLevelsAvailable)
                .AllowTransition(recordApplyingState, _ => true)
                .Build();
            
            AddState(recordApplyingState)
                .AllowTransition(topPlayersState, _ => true)
                .Build();
            
            AddState(topPlayersState)
                .AllowTransition(levelState, _ => true)
                .Build();
        }
    }
}