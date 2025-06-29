using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Enemies
{
    public class EnemySight : MonoBehaviour
    {
        [Header("Settings:")]
        [SerializeField] private Transform player;
        [SerializeField] private LayerMask obstacleMask; 
        [SerializeField] private LayerMask playerMask;   
        [SerializeField] private bool isSightRangeEnabled = true;
        [SerializeField] private float sightRange = 20f;
        [SerializeField] private bool isGizmosEnabled;

        public bool HasLineOfSight { get; private set; }
        
        public event UnityAction<bool> LineOfSightChanged;

        private void Update()
        {
            CheckLineOfSight();
        }

        private void CheckLineOfSight()
        {
            Vector3 directionToPlayer = (player.position + Vector3.up * 2 - transform.position).normalized ;
            float maxDistance = isSightRangeEnabled ? sightRange: Vector3.Distance(transform.position, player.position);

            // Check if the ray hits anything before reaching the player
            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, maxDistance, obstacleMask | playerMask))
            {
                // Check if hit object is player
                if (((1 << hit.collider.gameObject.layer) & playerMask) != 0)
                {
                    HasLineOfSight = true;
                    LineOfSightChanged?.Invoke(HasLineOfSight);
                }
                else
                {
                    HasLineOfSight = false;
                    LineOfSightChanged?.Invoke(HasLineOfSight);
                }
            }
            else
            {
                HasLineOfSight = false;
                LineOfSightChanged?.Invoke(HasLineOfSight);
            }
        }

        private void OnDrawGizmos()
        {
            if (isGizmosEnabled)
            {
                if (player != null)
                {
                    Gizmos.color = HasLineOfSight ? Color.green : Color.red;
                    Gizmos.DrawLine(transform.position, player.position + Vector3.up * 2);
                    Gizmos.DrawWireSphere(transform.position, sightRange);
                }
                else
                {
                    Debug.LogWarning("Player Transform is not assigned in EnemySight.");
                }
            }
        }
    }
}