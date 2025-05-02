using System;
using Game.Scripts.Enemies._BaseEnemy;
using RetroArsenal;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss
{
    public class ProjectileCollider : MonoBehaviour
    {
        [SerializeField] private EnemyAI enemyAI;    
    
        public event Action ProjectileTriggerEnter;
    
        private void Update()
        {
            RotateTowardTargetAroundY();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Projectile projectile))
            {
                Debug.Log("Projectile hit the collider: " + gameObject.name);
                ProjectileTriggerEnter?.Invoke();
            }
        }

        private void RotateTowardTargetAroundY()
        {
            var direction = (enemyAI.Target.position - enemyAI.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation.x = 0; // Keep the x rotation at 0
            lookRotation.z = 0; // Keep the z rotation at 0
            enemyAI.transform.rotation = lookRotation;
        }
    }
}