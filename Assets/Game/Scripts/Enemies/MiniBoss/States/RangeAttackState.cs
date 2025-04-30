using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss.States
{
    public class RangeAttackState : IState
    {
        private readonly Animator _animator;
        private readonly EnemyAI _enemyAI;

        private readonly int _rangeAttack = Animator.StringToHash("RangeAttack");
        
        public RangeAttackState(EnemyAI enemyAI, Animator animator)
        {
            _enemyAI = enemyAI;
            _animator = animator;
            // _attackRange = attackRange;
            // _attackCooldown = attackCooldown;
        }

        public void Enter()
        {
            _animator.Play(_rangeAttack);
            Debug.Log("Enter Range Attack State MINIBOSS");
        }

        public void Execute()
        {

        }

        public void Exit()
        {
          
        }
    }
}