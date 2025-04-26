using UnityEngine;

namespace Game.Scripts
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private GameObject shootPoint;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private int damage = 10;
        [SerializeField] private float attackCooldown = 3f;
        [SerializeField] private float speed = 10f;
        
        private float _lastAttackTime;

        public void Attack(Transform target)
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