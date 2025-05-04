using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss.States
{
    public class ShootState : IState
    {
        private readonly MiniBoss _miniBoss;
        private readonly MiniBossAI _enemyAI;
        private readonly Animator _animator;
        
        private readonly int _shootStack = Animator.StringToHash("Shoot_Stack");

        public ShootState(MiniBoss miniBoss, MiniBossAI enemyAI, Animator animator)
        {
            _miniBoss = miniBoss;
            _enemyAI = enemyAI;
            _animator = animator;
        }
        
        public void Enter()
        {
            _animator.Play(_shootStack);
        }

        public void Execute()
        {
            _miniBoss.RotateTowardTargetAroundY();
            // _enemyAI.Agent.SetDestination(_enemyAI.Target.position);
            // _enemyAI.Agent.updateRotation = true;
        }

        public void Exit()
        {
            
        }
    }
}