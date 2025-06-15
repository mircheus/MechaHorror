using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;

namespace Game.Scripts
{
    public class StateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private readonly EnemyAI _enemyAI;
        
        private IState _currentState;
        
        public StateMachine(EnemyAI enemyAI,  Dictionary<Type, IState> states)
        {
            _enemyAI = enemyAI;
            _states = states;
        }
        
        public void Update()
        {
            _currentState?.Execute();
        }
        
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }
        
        private TState ChangeState<TState>() where TState : class, IState
        {
            _currentState?.Exit();
            
            TState state = GetState<TState>();
            _currentState = state;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}