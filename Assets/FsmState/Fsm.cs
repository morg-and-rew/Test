using System;
using System.Collections.Generic;
using UnityEngine;

namespace Test.StateMachine
{
    public class Fsm : MonoBehaviour
    {
        private FsmState _currentState;
        private Type _currentStateType;

        private Dictionary<Type, FsmState> _states = new Dictionary<Type, FsmState>();

        public void AddState<T>(T state) where T : FsmState
        {
            Type type = typeof(T);

            if (!_states.ContainsKey(type))
                _states[type] = state;
        }

        public void SetState<T>() where T : FsmState
        {
            Type type = typeof(T);

            if (_currentStateType == type)
                return;

            if (_states.TryGetValue(type, out var newState))
            {
                _currentState?.Exit();

                _currentState = newState;
                _currentStateType = type;

                _currentState.Enter();
            }
        }

        public FsmState GetCurrentState()
        {
            return _currentState;
        }

        public void Update()
        {
            _currentState?.Update();
        }
    }
}
