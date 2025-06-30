using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.FourLeg.States
{
    public class IdleState : IState
    {
        private readonly EnemyAI _enemyAI;

        public IdleState(EnemyAI enemyAI)
        {
            _enemyAI = enemyAI;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        public void Execute()
        {
            if (Vector3.Distance(_enemyAI.transform.position, _enemyAI.Target.position) < _enemyAI.DetectionRange)
            {
                _enemyAI.InvokePlayerDetected();
                _enemyAI.StateMachine.Enter<AttackState>();
            }
        }
    }
}