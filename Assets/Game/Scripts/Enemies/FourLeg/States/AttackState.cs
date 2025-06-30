using System;
using System.Collections;
using DG.Tweening;
using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.FourLeg.States
{
    public class AttackState : IState
    {
        private readonly FourLegAI _enemyAI;
        private readonly FourLegAttack _attack;
        private readonly int _strafeDistanceMin;
        private readonly int _strafeDistanceMax;
        private Coroutine _strafeCoroutine;
        private Coroutine _attackCoroutine;
        private Vector3 _endPosition;
        private readonly bool _isStrafeEnabled;

        public AttackState(EnemyAI enemyAI)
        {
            _enemyAI = (FourLegAI)enemyAI;
            _attack = (FourLegAttack)_enemyAI.BaseEnemyAttack;
            _isStrafeEnabled = _enemyAI.IsStrafeEnabled;
            _strafeDistanceMin = (int)_enemyAI.StrafeDistanceMin;
            _strafeDistanceMax = (int)_enemyAI.StrafeDistanceMax;
        }

        public void Enter()
        {
            // Debug.Log("Entering Attack State");
            if (_isStrafeEnabled)
            {
                _strafeCoroutine = _enemyAI.StartCoroutine(Cooldown(Strafe, _enemyAI.StrafeCooldownTime));
            }
            _attackCoroutine = _enemyAI.StartCoroutine(Cooldown(Attack, _attack.AttackCooldown));
        }

        public void Execute()
        {
            RotateTowardTargetAroundY();

            if (_enemyAI.Agent.isStopped == false)
            {
                _enemyAI.Agent.SetDestination(_enemyAI.Target.transform.position);
            }
        }

        public void Exit()
        {
            // Debug.Log("Exiting Attack State");
            if (_isStrafeEnabled)
            {
                _enemyAI.StopCoroutine(_strafeCoroutine);
            }
            
            _enemyAI.StopCoroutine(_attackCoroutine);
        }

        private void Attack()
        {
            _attack.Attack(_enemyAI.Target);
        }

        private void Strafe()
        {
            int sideToStrafe = UnityEngine.Random.Range(0, 2);
            int doubleStrafe = UnityEngine.Random.Range(0, 2);
            Vector3 strafeDirection = sideToStrafe == 0 ? _enemyAI.transform.right : -_enemyAI.transform.right;
            Vector3 endPosition = _enemyAI.transform.position + strafeDirection * UnityEngine.Random.Range(_strafeDistanceMin, _strafeDistanceMax);
            
            _enemyAI.transform.DOMove(endPosition, 0.5f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                if (doubleStrafe == 1)
                {
                    Vector3 strafeDirection = sideToStrafe == 0 ? _enemyAI.transform.right : -_enemyAI.transform.right;
                    Vector3 endPosition = _enemyAI.transform.position + strafeDirection * UnityEngine.Random.Range(_strafeDistanceMin, _strafeDistanceMax);
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

        private void RotateTowardTarget()
        {
            Vector3 direction = (_enemyAI.Target.position - _enemyAI.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
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