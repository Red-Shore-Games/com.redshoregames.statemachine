using System;
using System.Collections.Generic;
using UnityEngine;

namespace RedShoreGames.StateMachine
{
    public class StateFactory<T> where T : MonoBehaviour
    {
        private readonly Dictionary<Type, State<T>> _cachedStates = new();

        /// <summary>
        /// Registers a new state in the factory.
        /// </summary>
        public void RegisterState<U>(U state) where U : State<T>
        {
            var stateType = typeof(U);
            if (!_cachedStates.ContainsKey(stateType))
            {
                _cachedStates[stateType] = state;
            }
        }

        /// <summary>
        /// Gets a registered state or creates it if it doesn't exist.
        /// </summary>
        public U GetState<U>() where U : State<T>, new()
        {
            var stateType = typeof(U);

            if (_cachedStates.TryGetValue(stateType, out var cachedState))
            {
                return (U)cachedState;
            }

            // State not found; create a new instance and cache it
            var newState = new U();
            _cachedStates[stateType] = newState;

            return newState;
        }
    }
}
