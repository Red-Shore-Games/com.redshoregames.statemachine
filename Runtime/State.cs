using UnityEngine;

namespace RedShoreGames.StateMachine
{
    public abstract class State<T> where T : MonoBehaviour
    {
        protected BaseStateMachine<T> stateMachine;

        public State(BaseStateMachine<T> stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public abstract void OnEnter();
        public abstract void OnUpdate(float deltaTime);
        public abstract void OnFixedUpdate(float deltaTime);
        public abstract void OnLateUpdate(float deltaTime);
        public abstract void OnExit();
        public abstract void OnDrawGizmos();
    }
}