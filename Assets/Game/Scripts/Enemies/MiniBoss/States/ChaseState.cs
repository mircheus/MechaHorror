using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss.States
{
    public class ChaseState : IState
    {
        private readonly MiniBossAI _enemyAI;
        private readonly Animator _animator;
        
        private readonly int _run = Animator.StringToHash("Run");
        private readonly int _idle = Animator.StringToHash("Idle");
        
        public ChaseState(MiniBossAI enemyAI, Animator animator)
        {
            _enemyAI = enemyAI;
            _animator = animator;
        }
        
        public void Enter()
        {
            _animator.Play(_run);
        }

        public void Execute()
        {
            // RotateTowardTargetAroundY();
            _enemyAI.Agent.SetDestination(_enemyAI.Target.position);
            _enemyAI.Agent.updateRotation = true;

            if (Vector3.Distance(_enemyAI.transform.position, _enemyAI.Target.position) < _enemyAI.Agent.stoppingDistance)
            {
                // _animator.Play(_idle);
                // _enemyAI.Agent.ResetPath();
                // _enemyAI.StateMachine.Enter<IdleState>();
                // _enemyAI.StateMachine.ChangeState(new AttackState(_enemyAI));
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
    }
}