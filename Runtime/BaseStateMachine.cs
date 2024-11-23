using RedShoreGames.EventSystem;
using UnityEngine;

namespace RedShoreGames.StateMachine
{
    public class BaseStateMachine<T> : Actor<T> where T : MonoBehaviour
    {
        #region Private Variables

        private State<T> _currentState;

        #endregion

        #region MonoBehaviour Methods

        private void Update()
        {
            _currentState?.OnUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _currentState?.OnFixedUpdate(Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            _currentState?.OnLateUpdate(Time.deltaTime);
        }

        private void OnDrawGizmos()
        {
            _currentState?.OnDrawGizmos();
        }

        #endregion

        #region Public Methods

        public void SwitchState(State<T> newState)
        {
            _currentState?.OnExit();
            _currentState = newState;
            _currentState.OnEnter();
        }

        #endregion
    }
}