using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Button = UnityEngine.UI.Button;

namespace Game.Scripts
{
    public class NavMeshUpdater : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface[] navMeshSurfaces;
        [SerializeField] private Transform target;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private bool debugLogInConsole = true;
        
        public void OnUpdateButtonClicked()
        {
            UpdateNavMeshSurfaces();
            Debug.Log("NavMesh updated");
        }

        private void Update()
        {
            CheckPathStatus();
        }

        private void UpdateNavMeshSurfaces()
        {
            foreach (var surface in navMeshSurfaces)
            {
                surface.UpdateNavMesh(surface.navMeshData);
            }
        }

        private void CheckPathStatus()
        {
            if (agent != null && debugLogInConsole)
            {
                // agent.SetDestination(target.position);

                // Check if the agent failed to find a path
                if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
                {
                    Debug.LogWarning("NavMeshAgent cannot find a path to the target!");
                }
                else if (agent.pathStatus == NavMeshPathStatus.PathPartial)
                {
                    Debug.LogWarning("NavMeshAgent found a partial path. Target might not be fully reachable.");
                }
            }
        }
    }
}