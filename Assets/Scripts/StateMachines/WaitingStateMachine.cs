using System;
using System.Threading.Tasks;
using UnityEngine;

namespace StateMachines
{
    public abstract class WaitingStateMachine<TStateType, TContext>
    {
        private readonly StateTransitionManager<TStateType, TContext> _transitionManager;
        private IAwaitableState<TStateType> _currentState;
        private TContext _context;

        protected WaitingStateMachine(StateTransitionManager<TStateType, TContext> transitionManager, TContext context)
        {
            _transitionManager = transitionManager;
            _context = context;
        }


        public async Task StartWithState(TStateType stateType)
        {
            _currentState = _transitionManager.GetStateByType(stateType);
            while (!_currentState.IsExitState)
            {
                Debug.Log($"Run State '{_currentState.Type}' in {GetType().Name}");
                await _currentState.RunState();
                _currentState = _transitionManager.GetNextState(_currentState, _context);
            }
            Debug.Log($"Finish State machine {GetType().Name}");
        }
    }

}