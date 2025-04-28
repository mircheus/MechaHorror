using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts
{
    public class AttackState : IState
    {
        private readonly EnemyAI _enemyAI;
        private Coroutine _strafeCoroutine;
        private Vector3 _endPosition;

        public AttackState(EnemyAI enemyAI)
        {
            _enemyAI = enemyAI;
        }

        public void Enter()
        {
            Debug.Log("Entering Attack State");
            // RotateTowardTarget();
            _strafeCoroutine = _enemyAI.StartCoroutine(Cooldown(Strafe, _enemyAI.StrafeCooldownTime));
        }

        public void Execute()
        {
            RotateTowardTargetAroundY();
            // StrafeToRight();
            _enemyAI.EnemyAttack.Attack(_enemyAI.Target);

            if (_enemyAI.Agent.isStopped == false)
            {
                _enemyAI.Agent.SetDestination(_enemyAI.Target.transform.position);
            }
            
            // if (Vector3.Distance(_enemyAI.transform.position, _enemyAI.target.position) > _enemyAI.attackRange)
            // {
            //     _enemyAI.StateMachine.ChangeState(new IdleState(_enemyAI));
            // }
        }

        public void Exit()
        {
            _enemyAI.StopCoroutine(_strafeCoroutine);
            Debug.Log("Exiting Attack State");
        }
        
        private void RotateTowardTarget()
        {
            Vector3 direction = (_enemyAI.Target.position - _enemyAI.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _enemyAI.transform.rotation = Quaternion.Slerp(_enemyAI.transform.rotation, lookRotation, Time.deltaTime * _enemyAI.RotationSpeed);
        }

        private void Strafe()
        {
            int sideToStrafe = UnityEngine.Random.Range(0, 2);
            int doubleStrafe = UnityEngine.Random.Range(0, 2);
            Vector3 strafeDirection = sideToStrafe == 0 ? _enemyAI.transform.right : -_enemyAI.transform.right;
            Vector3 endPosition = _enemyAI.transform.position + strafeDirection * 20f;
            
            _enemyAI.transform.DOMove(endPosition, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                if (doubleStrafe == 1)
                {
                    Vector3 strafeDirection = sideToStrafe == 0 ? _enemyAI.transform.right : -_enemyAI.transform.right;
                    Vector3 endPosition = _enemyAI.transform.position + strafeDirection * 10f;
                    _enemyAI.transform.DOMove(endPosition, 0.5f).SetEase(Ease.OutQuad);
                }
            });
        }
        
        private void MoveToDestination(Vector3 destination)
        {
            _enemyAI.Agent.SetDestination(destination);
            
        }
        
        private void RotateTowardTargetAroundY()
        {
            Vector3 direction = (_enemyAI.Target.position - _enemyAI.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation.x = 0; // Keep the x rotation at 0
            lookRotation.z = 0; // Keep the z rotation at 0
            _enemyAI.transform.rotation = Quaternion.Slerp(_enemyAI.transform.rotation, lookRotation, Time.deltaTime * _enemyAI.RotationSpeed);
        }
        
        private IEnumerator Cooldown(Action action, float cooldownTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(cooldownTime);
                action();
            }
            
            yield break;
        }
    }
}