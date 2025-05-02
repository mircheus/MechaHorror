using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss.States
{
    public class IdleState : IState
    {
        private readonly MiniBoss _miniBoss;
        private readonly MiniBossAI _enemyAI;
        private readonly Animator _animator;
        
        private readonly int _idleHash = Animator.StringToHash("Idle");

        public IdleState(MiniBoss miniBoss, MiniBossAI enemyAI, Animator animator)
        {
            _enemyAI = enemyAI;
            _animator = animator;
            _miniBoss = miniBoss;
        }
        
        public void Enter()
        {
            _animator.Play(_idleHash);
        }

        public void Execute()
        {
            RotateTowardTargetAroundY();
            // if ()
            // {
            //     _enemyAI.StateMachine.Enter<ChaseState>();
            // }
        }

        public void Exit()
        {
        }

        // private bool IsInsideIdleCircle()
        // {
        //     // var distance Vector3.Distance(_enemyAI.transform.position, _enemyAI.Target.position) < _enemyAI.OuterCircle &&
        // }
        
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