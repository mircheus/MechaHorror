using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.Footballer.States
{
    public class AttackState : IState
    {
        private readonly Animator _animator;

        private EnemyAI _enemyAI;
        private GameObject _ballPrefab;
        private int _attackTrigger = Animator.StringToHash("Attack");

        public AttackState(EnemyAI enemyAI, Animator animator)
        {
            _enemyAI = enemyAI;
            _animator = animator;
        }

        public void Enter()
        {
            _animator.SetTrigger(_attackTrigger);
        }

        public void Execute()
        {
            LookOnTarget();
        }

        public void Exit()
        {

        }

        private void LookOnTarget()
        {
            // _enemyAI.transform.LookAt(_enemyAI.Target);
            // var direction = _enemyAI.Target.position - _enemyAI.transform.position;
            // var rotation = Quaternion.LookRotation(direction, Vector3.up);
            // var baseRotation = _enemyAI.transform.rotation;
            // _enemyAI.transform.rotation = new Quaternion(baseRotation.x, rotation.y, baseRotation.z, baseRotation.w);
            Vector3 direction = _enemyAI.Target.position - _enemyAI.transform.position;

            // Ignore Y difference to keep only horizontal direction
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                _enemyAI.transform.rotation = Quaternion.Slerp(_enemyAI.transform.rotation, lookRotation, Time.deltaTime * 5f); // Smooth turning
            }
        }
    }
}