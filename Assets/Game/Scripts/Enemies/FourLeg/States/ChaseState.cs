using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.FourLeg.States
{
    public class ChaseState : IState
    {
        private readonly EnemyAI _enemyAI;
        private readonly EnemySight _enemySight;

        public ChaseState(EnemyAI enemyAI, EnemySight enemySight)
        {
            _enemyAI = enemyAI;
            _enemySight = enemySight;
        }

        public void Enter()
        {
        }

        public void Execute()
        {
            _enemyAI.Agent.SetDestination(_enemyAI.Target.position);

            if (Vector3.Distance(_enemyAI.transform.position, _enemyAI.Target.position) < _enemyAI.AttackRange)
            {
                _enemyAI.Agent.ResetPath();
                _enemyAI.StateMachine.Enter<AttackState>();
            }

            if (_enemySight.HasLineOfSight)
            {
                _enemyAI.StateMachine.Enter<AttackState>();
            }
        }

        public void Exit()
        {
        }
    }
}