using UnityEngine;

namespace Game.Scripts
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
            Debug.Log("Entering Idle State");
        }

        public void Exit()
        {
            Debug.Log("Exiting Idle State");
        }

        public void Execute()
        {
            if (Vector3.Distance(_enemyAI.transform.position, _enemyAI.target.position) < _enemyAI.detectionRange)
            {
                _enemyAI.StateMachine.ChangeState(new AttackState(_enemyAI));
            }
        }
    }
}