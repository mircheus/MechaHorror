using UnityEngine;

namespace Game.Scripts
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
            _enemyAI.agent.SetDestination(_enemyAI.target.position);

            if (Vector3.Distance(_enemyAI.transform.position, _enemyAI.target.position) < _enemyAI.attackRange)
            {
                _enemyAI.agent.ResetPath();
                _enemyAI.StateMachine.ChangeState(new AttackState(_enemyAI));
            }
        }

        public void Exit()
        {
            Debug.Log("Exiting Chase State");
        }
    }
}