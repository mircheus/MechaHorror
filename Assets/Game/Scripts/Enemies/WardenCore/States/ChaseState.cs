using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.FourLegEnemy.States;
using UnityEngine;

namespace Game.Scripts.Enemies.WardenCore.States
{
    public class ChaseState : IState
    {
        private readonly EnemyAI _enemyAI;

        public ChaseState(EnemyAI enemyAI)
        {
            _enemyAI = enemyAI;
        }

        public void Enter()
        {
            Debug.Log("Entering Chase State");
        }

        public void Execute()
        {
            _enemyAI.Agent.SetDestination(_enemyAI.Target.position);

            if (Vector3.Distance(_enemyAI.transform.position, _enemyAI.Target.position) < _enemyAI.AttackRange)
            {
                _enemyAI.Agent.ResetPath();
                // _enemyAI.StateMachine.ChangeState(new AttackState(_enemyAI));
                _enemyAI.StateMachine.Enter<AttackState>();
            }
        }

        public void Exit()
        {
            Debug.Log("Exiting Chase State");
        }
    }
}