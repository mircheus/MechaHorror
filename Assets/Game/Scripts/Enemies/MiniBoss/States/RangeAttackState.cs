using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss.States
{
    public class RangeAttackState : IState
    {
        private readonly Animator _animator;
        private readonly MiniBossAI _enemyAI;

        private readonly int _rangeAttack = Animator.StringToHash("RangeAttack");

        public RangeAttackState(MiniBossAI enemyAI, Animator animator)
        {
            _enemyAI = enemyAI;
            _animator = animator;
            // _attackRange = attackRange;
            // _attackCooldown = attackCooldown;
        }

        public void Enter()
        {
            _animator.Play(_rangeAttack);
        }

        public void Execute()
        {
            RotateTowardTargetAroundY();
            
            if (IsOutOfRange()) // Не имеет особого смысла тк это RangeAttack, но для теста пусть будет 
            {
                _enemyAI.StateMachine.Enter<IdleState>();
            }
        }

        public void Exit()
        {

        }

        private void RotateTowardTargetAroundY()
        {
            Vector3 direction;
            
            if (_enemyAI.IsRangeAttackDirectionForward)
            {
                direction = _enemyAI.transform.forward;
            }
            else
            {
                direction = (_enemyAI.Target.position - _enemyAI.transform.position).normalized;
            }
            
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation.x = 0; // Keep the x rotation at 0
            lookRotation.z = 0; // Keep the z rotation at 0
            _enemyAI.transform.rotation = Quaternion.Slerp(_enemyAI.transform.rotation, lookRotation,
                Time.deltaTime * _enemyAI.RotationSpeed);
        }

        private bool IsOutOfRange() => Vector3.Distance(_enemyAI.transform.position, _enemyAI.Target.position) > _enemyAI.ExitFromRangeAttackRange;
    }
}