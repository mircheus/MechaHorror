using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss.States
{
    public class IdleState : IState
    {
        private readonly EnemyAI _enemyAI;
        private readonly Animator _animator;
        
        private readonly int _idleHash = Animator.StringToHash("Idle");

        public IdleState(EnemyAI enemyAI, Animator animator)
        {
            _enemyAI = enemyAI;
            _animator = animator;
        }
        
        public void Enter()
        {
            _animator.Play(_idleHash);
            Debug.Log("Enter Idle State MINIBOSS");
        }

        public void Execute()
        {
            if (Vector3.Distance(_enemyAI.transform.position, _enemyAI.Target.position) < _enemyAI.DetectionRange)
            {
                // _enemyAI.StateMachine.ChangeState(new AttackState(_enemyAI));
                _enemyAI.StateMachine.Enter<RangeAttackState>();
            }
        }

        public void Exit()
        {
        }
    }
}