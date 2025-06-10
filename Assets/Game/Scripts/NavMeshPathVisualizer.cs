using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts
{
    public class NavMeshPathVisualizer : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Color pathColor = Color.magenta;

        void OnDrawGizmos()
        {
            if (agent == null || agent.path == null)
                return;

            if (agent.hasPath)
            {
                Handles.color = pathColor;
                var path = agent.path;
                for (int i = 0; i < path.corners.Length - 1; i++)
                {
                    Handles.DrawLine(path.corners[i], path.corners[i + 1], 6f);
                }
            }
        }
    }
}