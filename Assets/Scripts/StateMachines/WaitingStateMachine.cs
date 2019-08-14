using System.Threading.Tasks;

namespace StateMachines
{
    public abstract class WaitingStateMachine<TStateType, TContext>
    {
        private readonly StateTransitionManager<TStateType, TContext> _transitionManager;
        private IAwaitableState<TStateType> _currentState;
        private TContext _context;

        public async Task StartWithState(TStateType stateType)
        {
            _currentState = _transitionManager.GetStateByType(stateType);
            while (!_currentState.IsExitState)
            {
                await _currentState.RunState();
                _currentState = _transitionManager.GetNextState(_currentState, _context);
            }
        }
    }
}