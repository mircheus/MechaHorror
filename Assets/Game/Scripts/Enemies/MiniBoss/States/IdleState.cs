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
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }
    }
}