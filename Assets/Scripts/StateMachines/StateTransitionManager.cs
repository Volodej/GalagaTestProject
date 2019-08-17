using System;
using System.Collections.Generic;
using System.Linq;
using LifeCycle.Game.States;
using UnityEngine;

namespace StateMachines
{
    public class StateTransitionManager<TStateType, TContext>
    {
        private readonly Dictionary<TStateType, StateTransitionsHolder<TStateType, TContext>> _transitionHolders =
            new Dictionary<TStateType, StateTransitionsHolder<TStateType, TContext>>();

        public IAwaitableState<TStateType> GetStateByType(TStateType stateType) => _transitionHolders[stateType].State;

        public IAwaitableState<TStateType> GetNextState(IAwaitableState<TStateType> currentState, TContext context)
        {
            var nextState = _transitionHolders[currentState.Type].Transitions
                .First(transition => transition.CanSelectPredicate(context))
                .NextState;
            
            Debug.Log($"({GetType().Name}); current: {currentState.Type}; next: {nextState.Type}\nContext: {context}");

            return nextState;
        }

        protected HolderBuilder AddState(IAwaitableState<TStateType> state)
        {
            return new HolderBuilder(this, state);
        }

        public class HolderBuilder
        {
            private readonly StateTransitionManager<TStateType, TContext> _stateTransitionManager;
            private readonly IAwaitableState<TStateType> _state;

            private readonly List<StateTransitionsHolder<TStateType, TContext>.StateTransition> _transitions =
                new List<StateTransitionsHolder<TStateType, TContext>.StateTransition>();

            public HolderBuilder(StateTransitionManager<TStateType, TContext> stateTransitionManager, IAwaitableState<TStateType> state)
            {
                _stateTransitionManager = stateTransitionManager;
                _state = state;
            }

            public HolderBuilder AllowTransition(IAwaitableState<TStateType> newState, Func<TContext, bool> predicate)
            {
                _transitions.Add(new StateTransitionsHolder<TStateType, TContext>.StateTransition(predicate, newState));
                return this;
            }

            public void Build()
            {
                _stateTransitionManager._transitionHolders.Add(_state.Type,
                    new StateTransitionsHolder<TStateType, TContext>(_state, _transitions));
            }
        }
    }
}