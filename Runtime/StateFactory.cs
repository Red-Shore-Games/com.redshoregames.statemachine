using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RedShoreGames.StateMachine
{
    public class StateFactory<T> where T : MonoBehaviour
    {
        private readonly Dictionary<Type, Func<State<T>>> _stateFactories = new();

        public StateFactory(BaseStateMachine<T> stateMachine)
        {
            List<Type> stateTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(State<T>)))
                .ToList();

            foreach (Type type in stateTypes)
            {
                _stateFactories[type] = () => (State<T>)Activator.CreateInstance(type, stateMachine);
            }
        }

        public U GetState<U>() where U : State<T>
        {
            if (_stateFactories.TryGetValue(typeof(U), out var stateFactory))
            {
                return (U)stateFactory();
            }

            throw new InvalidOperationException($"State of type {typeof(U).Name} is not registered.");
        }
    }
}
