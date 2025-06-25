using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.Footballer.States
{
    public class IdleState : IState
    {
        private readonly EnemyAI _enemyAI;
        private readonly Animator _animator;
        private readonly int _idleTrigger = Animator.StringToHash("Idle");

        public IdleState(EnemyAI enemyAI, Animator animator)
        {
            _enemyAI = (FootballerAI)enemyAI;
            _animator = animator; // Uncomment if you need to use the animator in this state
        }

        public void Enter()
        {
            _animator.SetTrigger(_idleTrigger);
        }

        public void Execute()
        {
            // if (Vector3.Distance(_enemyAI.transform.position, _enemyAI.Target.position) < _enemyAI.DetectionRange)
            // {
            //     _enemyAI.StateMachine.Enter<AttackState>();
            // }
        }

        public void Exit()
        {
        }
    }
}