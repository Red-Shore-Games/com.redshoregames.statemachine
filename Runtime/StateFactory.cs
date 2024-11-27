using System;
using System.Collections.Generic;
using UnityEngine;

namespace RedShoreGames.StateMachine
{
    public class StateFactory<T> where T : MonoBehaviour
    {
        private readonly Dictionary<Type, State<T>> _cachedStates = new();

        public void RegisterState<U>(U state) where U : State<T>
        {
            var stateType = typeof(U);
            if (!_cachedStates.ContainsKey(stateType))
            {
                _cachedStates[stateType] = state;
            }
        }

        public U GetState<U>() where U : State<T>
        {
            Type stateType = typeof(U);

            if (_cachedStates.TryGetValue(stateType, out var cachedState))
            {
                return (U)cachedState;
            }

            throw new InvalidOperationException($"State of type {stateType} has not been registered.");
        }
    }
}
