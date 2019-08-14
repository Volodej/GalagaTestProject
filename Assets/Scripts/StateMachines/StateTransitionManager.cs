namespace StateMachines
{
    public class StateTransitionManager<TStateType, TContext>
    {
        public IAwaitableState<TStateType> GetStateByType(TStateType stateType)
        {
            throw new System.NotImplementedException();
        }

        public IAwaitableState<TStateType> GetNextState(IAwaitableState<TStateType> currentState, TContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}