using System.Collections;
using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.FourLeg.States
{
    public class ChaseState : IState
    {
        private readonly EnemyAI _enemyAI;
        private readonly EnemySight _enemySight;
        private bool _isCooldown = true;

        public ChaseState(EnemyAI enemyAI, EnemySight enemySight)
        {
            _enemyAI = enemyAI;
            _enemySight = enemySight;
        }

        public void Enter()
        {
            // Debug.Log("Entering Chase State");
            _enemyAI.StartCoroutine(EnterStateCooldown());
        }

        public void Execute()
        {
            _enemyAI.Agent.SetDestination(_enemyAI.Target.position);

            if (Vector3.Distance(_enemyAI.transform.position, _enemyAI.Target.position) < _enemyAI.AttackRange)
            {
                _enemyAI.Agent.ResetPath();
                _enemyAI.StateMachine.Enter<AttackState>();
            }

            if (_enemySight.HasLineOfSight && _isCooldown == false)
            {
                _enemyAI.StateMachine.Enter<AttackState>();
            }
        }

        public void Exit()
        {
            // Debug.Log("Exiting Chase State");
        }

        private IEnumerator EnterStateCooldown()
        {
            yield return new WaitForSeconds(2f);
            _isCooldown = false;
        }
    }
}