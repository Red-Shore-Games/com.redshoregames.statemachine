using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RedShoreGames.StateMachine
{
    public class StateFactory<T> where T : MonoBehaviour
    {
        private readonly Dictionary<Type, Lazy<State<T>>> _states = new();

        public StateFactory(BaseStateMachine<T> stateMachine)
        {
            List<Type> stateTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(State<T>)))
                .ToList();

            foreach (Type type in stateTypes)
            {
                _states[type] = new Lazy<State<T>>(() => (State<T>)Activator.CreateInstance(type, stateMachine));
            }
        }

        public U GetState<U>() where U : State<T>
        {
            if (_states.TryGetValue(typeof(U), out Lazy<State<T>> lazyState))
            {
                return (U)lazyState.Value;
            }

            throw new InvalidOperationException($"State of type {typeof(U).Name} is not registered.");
        }
    }
}