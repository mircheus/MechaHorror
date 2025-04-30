using System;
using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss
{
    public class MiniBossAttack : BaseEnemyAttack
    {
        [SerializeField] private MiniBossAI enemyAI;
        [SerializeField] private RangeAttackProjectile rangeAttackPrefab;
        [SerializeField] private AnimationEventInvoker animationEventInvoker;

        private void OnEnable()
        {
            if (animationEventInvoker != null)
            {
                animationEventInvoker.OnRangeAttack += HandleRangeAttack;
            }
        }

        private void OnDisable()
        {
            if (animationEventInvoker != null)
            {
                animationEventInvoker.OnRangeAttack -= HandleRangeAttack;
            }
        }

        public override void Attack(Transform target)
        {
            // Implement the attack logic here
            // For example, deal damage to the target
            // target.GetComponent<Health>().TakeDamage(damage);
        }

        public void RangeAttack(Transform target)
        {
            if (rangeAttackPrefab != null)
            {
                RangeAttackProjectile projectile = Instantiate(rangeAttackPrefab, transform.position, Quaternion.identity);
                projectile.Initialize(target);
            }
            else
            {
                Debug.LogWarning("Range attack prefab is not assigned.");
            }
        }

        private void HandleRangeAttack()
        {
            if (enemyAI.Target != null)
            {
                RangeAttack(enemyAI.Target);
            }
            else
            {
                Debug.LogWarning("Target is not assigned.");
            }
        }
    }
}