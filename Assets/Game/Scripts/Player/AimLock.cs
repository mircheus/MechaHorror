using System;
using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Player
{
    [RequireComponent(typeof(SphereCollider))]
    public class AimLock : MonoBehaviour
    {
        [SerializeField] private float detectionRadius = 5f;
        
        private SphereCollider _sphereCollider;
        
        public event UnityAction<Transform> TargetDetected;
        public event UnityAction TargetLost;

        private void Start()
        {
            _sphereCollider = GetComponent<SphereCollider>();
            _sphereCollider.radius = detectionRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BaseEnemy enemy))
            {
                TargetDetected?.Invoke(enemy.transform);
                enemy.Death += OnEnemyDeath;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out BaseEnemy enemy))
            {
                TargetLost?.Invoke();
                enemy.Death -= OnEnemyDeath;
            }
        }

        private void OnEnemyDeath()
        {
            TargetLost?.Invoke();
        }
    }
}