using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.Footballer.States
{
    public class AttackState : IState
    {
        private readonly Animator _animator;
        
        private EnemyAI _enemyAI;
        private GameObject _ballPrefab;
        private int _attack = Animator.StringToHash("Attack");

        public AttackState(EnemyAI enemyAI, Animator animator)
        {
            _enemyAI = enemyAI;
            _animator = animator;
        }
        
        public void Enter()
        {
           _animator.SetTrigger(_attack);
        }

        public void Execute()
        {
           LookOnTarget();
        }

        public void Exit()
        {
           
        }

        private void LookOnTarget()
        {
            _enemyAI.transform.LookAt(_enemyAI.Target);
        }
    }
}