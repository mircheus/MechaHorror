using UnityEngine;

namespace Game.Scripts
{
    public class AttackState : IState
    {
        private readonly EnemyAI _enemyAI;

        public AttackState(EnemyAI enemyAI)
        {
            _enemyAI = enemyAI;
        }

        public void Enter()
        {
            Debug.Log("Entering Attack State");
            // RotateTowardTarget();
        }

        public void Execute()
        {
            RotateTowardTargetAroundY();
            _enemyAI.EnemyAttack.Attack(_enemyAI.target);
            
            if (Vector3.Distance(_enemyAI.transform.position, _enemyAI.target.position) > _enemyAI.attackRange)
            {
                _enemyAI.StateMachine.ChangeState(new ChaseState(_enemyAI));
            }
        }

        public void Exit()
        {
            Debug.Log("Exiting Attack State");
        }
        
        private void RotateTowardTarget()
        {
            Vector3 direction = (_enemyAI.target.position - _enemyAI.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _enemyAI.transform.rotation = Quaternion.Slerp(_enemyAI.transform.rotation, lookRotation, Time.deltaTime * _enemyAI.rotationSpeed);
        }

        private void RotateTowardTargetAroundY()
        {
            Vector3 direction = (_enemyAI.target.position - _enemyAI.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation.x = 0; // Keep the x rotation at 0
            lookRotation.z = 0; // Keep the z rotation at 0
            _enemyAI.transform.rotation = Quaternion.Slerp(_enemyAI.transform.rotation, lookRotation, Time.deltaTime * _enemyAI.rotationSpeed);
        }
    }
}