using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.FourLegEnemy
{
    public class FourLegEnemyAttack : EnemyAttackBase
    {
        [SerializeField] private GameObject shootPoint;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float speed = 10f;
        
        private float _lastAttackTime;

        public override void Attack(Transform target)
        {
            if (Time.time >= _lastAttackTime + attackCooldown)
            {
                Vector3 spawnPositionWithOffset = shootPoint.transform.position;
                var direction = target.position - spawnPositionWithOffset;
                GameObject projectile = Instantiate(projectilePrefab, spawnPositionWithOffset, Quaternion.identity);
                projectile.transform.LookAt(spawnPositionWithOffset + direction * 10f);
                projectile.GetComponent<Rigidbody>().AddForce(direction * speed);
                
                _lastAttackTime = Time.time;
            }
        }
    }
}