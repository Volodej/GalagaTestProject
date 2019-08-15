using System;
using System.Collections.Generic;

namespace StateMachines
{
    public class StateTransitionsHolder<TStateType, TContext>
    {
        public IAwaitableState<TStateType> State { get; }
        public List<StateTransition> Transitions { get; }

        public StateTransitionsHolder(IAwaitableState<TStateType> state, List<StateTransition> transitions)
        {
            State = state;
            Transitions = transitions;
        }

        public class StateTransition
        {
            public Func<TContext, bool> CanSelectPredicate { get; }
            public IAwaitableState<TStateType> NextState { get; }

            public StateTransition(Func<TContext, bool> canSelectPredicate, IAwaitableState<TStateType> nextState)
            {
                CanSelectPredicate = canSelectPredicate;
                NextState = nextState;
            }
        }
        
    }
}