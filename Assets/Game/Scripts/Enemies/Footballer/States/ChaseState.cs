using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.Footballer.States
{
    public class ChaseState : IState
    {
        private readonly FootballerAI _enemyAI;
        private readonly Animator _animator;
        private readonly int _chaseTrigger = Animator.StringToHash("Chase");
        
        public ChaseState(EnemyAI enemyAI, Animator animator)
        {
            _enemyAI = (FootballerAI)enemyAI;
            _animator = animator;
        }
        
        public void Enter()
        {
            _animator.SetTrigger(_chaseTrigger);
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }
    }
}