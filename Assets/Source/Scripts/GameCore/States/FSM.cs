using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.GameCore.States
{
    public class FSM
    {
        private Dictionary<Type, FSMState> _states;
        private FSMState _currentState;
        
        public void Initialize<TDefaultState>(Dictionary<Type, FSMState> states) where TDefaultState : FSMState
        {
            _states = states;
            _currentState = _states[typeof(TDefaultState)];
            _currentState.Enter();
        }

        public void Set<T>() where T : FSMState
        {
            if (_states.TryGetValue(typeof(T), out FSMState state) == false)
            {
                Debug.LogError($"State {typeof(T)} does not exist.");
                return;
            }
            
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Update() => 
            _currentState?.Update();
    }
}