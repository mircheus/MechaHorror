using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Player
{
    [RequireComponent(typeof(SphereCollider))]
    public class AimLock : MonoBehaviour
    {
        [SerializeField] private float detectionRadius = 5f;
        [SerializeField] private float dotThreshold = .5f;
        
        private List<BaseEnemy> _detectedEnemies = new List<BaseEnemy>();
        private SphereCollider _sphereCollider;

        public event UnityAction<Transform> TargetDetected;
        public event UnityAction TargetLost;

        private void Start()
        {
            _sphereCollider = GetComponent<SphereCollider>();
            _sphereCollider.radius = detectionRadius;
        }

        private void Update()
        {
            if (_detectedEnemies.Count > 0)
            {
                DetectNearestEnemy();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BaseEnemy enemy))
            {
                _detectedEnemies.Add(enemy);
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
                _detectedEnemies.Remove(enemy);
            }
        }

        private void OnEnemyDeath()
        {
            TargetLost?.Invoke();
        }

        private void DetectNearestEnemy()
        {
            float distance = -1;

            foreach (var detectedEnemy in _detectedEnemies)
            {
                float currentDistance = Vector3.Distance(transform.position, detectedEnemy.transform.position);
                
                if(IsEnemyVisible(detectedEnemy.transform))
                {
                    if (distance < 0 || currentDistance < distance)
                    {
                        distance = currentDistance;
                        TargetDetected?.Invoke(detectedEnemy.transform);
                    }
                }
            }
        }

        private bool IsEnemyVisible(Transform enemy)
        {
            var transform1 = transform;
            Vector3 directionToEnemy = enemy.position - transform1.position;
            // float dot = Vector3.Dot(transform1.forward, directionToEnemy.normalized);
            // return dot > dotThreshold; // Adjust this threshold as needed
            
            float angle = Vector3.Angle(transform1.forward, directionToEnemy);
            return angle < 60f;
        }
    }
}