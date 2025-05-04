using System;
using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss
{
    public class MiniBossAttack : BaseEnemyAttack
    {
        [SerializeField] private MiniBossAI enemyAI;
        [SerializeField] private RangeAttackProjectile rangeAttackPrefab;
        [SerializeField] private RangeAttackProjectile shootAttackPrefab;
        [SerializeField] private ParticleSystem shootAttackFX;
        [SerializeField] private Transform rangeAttackSpawnPoint;
        [SerializeField] private Transform shootAttackSpawnPoint;
        [SerializeField] private AnimationEventInvoker animationEventInvoker;

        public event Action RangeAttackPerformed;
        
        private void OnEnable()
        {
            if (animationEventInvoker != null)
            {
                animationEventInvoker.OnRangeAttack += HandleAnimationEvent;
            }
        }

        private void OnDisable()
        {
            if (animationEventInvoker != null)
            {
                animationEventInvoker.OnRangeAttack -= HandleAnimationEvent;
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
                RangeAttackProjectile projectile = Instantiate(rangeAttackPrefab, rangeAttackSpawnPoint.position, Quaternion.identity);
                projectile.ShootTo(target);
                RangeAttackPerformed?.Invoke();
                
                // projectile.ShootTo(transform.forward);
            }
            else
            {
                Debug.LogWarning("Range attack prefab is not assigned.");
            }
        }
        
        public void ShootAttack(Transform target)
        {
            if (shootAttackPrefab != null)
            {
                shootAttackFX.Play();
                RangeAttackProjectile projectile = Instantiate(shootAttackPrefab, shootAttackSpawnPoint.position, Quaternion.identity);
                projectile.ShootTo(target);
                // projectile.ShootTo(transform.forward);
            }
            else
            {
                Debug.LogWarning("Shoot attack prefab is not assigned.");
            }
        }

        private void HandleAnimationEvent(AttackType attackType)
        {
            switch (attackType)
            {
                case AttackType.RangeAttack:
                    RangeAttack(enemyAI.Target);
                    break;
                case AttackType.ShootAttack:
                    ShootAttack(enemyAI.Target);
                    break;
                default:
                    Debug.LogWarning("Unknown attack type.");
                    break;
            }
        }
    }
}