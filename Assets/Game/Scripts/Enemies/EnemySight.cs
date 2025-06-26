using System;
using UnityEngine;

namespace Game.Scripts.Enemies
{
    public class EnemySight : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float sightRange = 20f;
        [SerializeField] private LayerMask obstacleMask; // Layer for walls/obstacles
        [SerializeField] private LayerMask playerMask;   // Layer for the player

        public bool HasLineOfSight { get; private set; }

        void Update()
        {
            CheckLineOfSight();
        }

        private void CheckLineOfSight()
        {
            Vector3 directionToPlayer = (player.position + Vector3.up * 2 - transform.position).normalized ;
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Check if the ray hits anything before reaching the player
            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, distanceToPlayer + 5f, obstacleMask | playerMask))
            {
                // Check if hit object is player
                if (((1 << hit.collider.gameObject.layer) & playerMask) != 0)
                // if (hit.collider.gameObject.layer == playerMask)
                {
                    HasLineOfSight = true;
                }
                else
                {
                    HasLineOfSight = false;
                }
            }
            else
            {
                HasLineOfSight = false;
            }
        }

        private void OnDrawGizmos()
        {
            CheckLineOfSight();
            
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