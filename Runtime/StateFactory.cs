using System;
using System.Collections.Generic;
using UnityEngine;

namespace RedShoreGames.StateMachine
{
    public class StateFactory<T> where T : MonoBehaviour
    {
        private readonly Dictionary<Type, State<T>> _cachedStates = new();
        private readonly BaseStateMachine<T> _stateMachine;

        public StateFactory(BaseStateMachine<T> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public U GetState<U>() where U : State<T>
        {
            Type stateType = typeof(U);

            if (_cachedStates.TryGetValue(stateType, out var cachedState))
            {
                return (U)cachedState;
            }

            var newState = (U)Activator.CreateInstance(typeof(U), _stateMachine);
            _cachedStates[stateType] = newState;
            return newState;
        }
    }
}
