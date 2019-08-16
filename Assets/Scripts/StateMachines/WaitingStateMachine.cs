using System;
using System.Threading.Tasks;
using UnityEngine;

namespace StateMachines
{
    public abstract class WaitingStateMachine<TStateType, TContext>
    {
        private readonly StateTransitionManager<TStateType, TContext> _transitionManager;
        private readonly TContext _context;
        private IAwaitableState<TStateType> _currentState;

        protected WaitingStateMachine(StateTransitionManager<TStateType, TContext> transitionManager, TContext context)
        {
            _transitionManager = transitionManager;
            _context = context;
        }

        public async Task StartWithState(TStateType stateType)
        {
            _currentState = _transitionManager.GetStateByType(stateType);
            await _currentState.RunState();
            do
            {
                Debug.Log($"Finished State '{_currentState.Type}' in {GetType().Name}");
                _currentState = _transitionManager.GetNextState(_currentState, _context);
                await _currentState.RunState();
            } while (!_currentState.IsExitState);
            Debug.Log($"Finish State machine {GetType().Name}");
        }
    }
}