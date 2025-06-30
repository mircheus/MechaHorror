using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.Footballer.States
{
    public class AttackState : IState
    {
        private readonly Animator _animator;

        private FootballerAI _enemyAI;
        private GameObject _ballPrefab;
        private int _attackTrigger = Animator.StringToHash("Attack");

        public AttackState(EnemyAI enemyAI, Animator animator)
        {
            _enemyAI = (FootballerAI)enemyAI;
            _animator = animator;
        }

        public void Enter()
        {
            // _animator.SetTrigger(_attackTrigger);
            _animator.Play(_attackTrigger);
            _enemyAI.EnemySight.LineOfSightChanged += OnLineOfSightChanged;
        }

        public void Execute()
        {
            LookOnTarget();
        }

        public void Exit()
        {
            _enemyAI.EnemySight.LineOfSightChanged -= OnLineOfSightChanged;
        }

        private void OnLineOfSightChanged(bool hasLineOfSight)
        {
            // Debug.Log("AttackState: OnLineOfSightChanged called. Has line of sight: " + hasLineOfSight);
            //
            // if (hasLineOfSight == false)
            // {
            //     _enemyAI.StateMachine.Enter<IdleState>();
            // }
        }

        private void LookOnTarget()
        {
            Vector3 direction = _enemyAI.Target.position - _enemyAI.transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                _enemyAI.transform.rotation = Quaternion.Slerp(_enemyAI.transform.rotation, lookRotation, Time.deltaTime * 5f); // Smooth turning
            }
        }
    }
}