using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.FourLegEnemy
{
    public class FourLegAttack : BaseEnemyAttack
    {
        [SerializeField] private GameObject[] shootPoint;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float speed = 10f;

        private int _currentShootPointIndex = 0;
        private float _lastAttackTime;

        public override void Attack(Transform target)
        {
            
            // if (Time.time >= _lastAttackTime + attackCooldown)
            // {
                Vector3 spawnPositionWithOffset = shootPoint[0].transform.position;
                var direction = target.position - spawnPositionWithOffset;
                GameObject projectile = Instantiate(projectilePrefab, spawnPositionWithOffset, Quaternion.identity);
                projectile.transform.LookAt(spawnPositionWithOffset + direction * 10f);
                projectile.GetComponent<Rigidbody>().AddForce(direction * speed);
                
            ChangeShootPoint();
                // _lastAttackTime = Time.time;
            // }
        }

        private void ChangeShootPoint()
        {
            if (_currentShootPointIndex == 0)
            {
                _currentShootPointIndex = 1;
            }
            else
            {
                _currentShootPointIndex = 0;
            }
        }
    }
}