using UnityEngine;

namespace Game.Scripts.Enemies.WardenCore.States.StateData
{
    public struct PatrolSettings
    {
        public Transform[] patrolPoints;
        public float patrolRadius;
        public float waitTimeAtPoint;
        public float pointReachThreshold;

        public PatrolSettings(Transform[] patrolPoints, float patrolRadius, float waitTimeAtPoint, float pointReachThreshold)
        {
            this.patrolPoints = patrolPoints;
            this.patrolRadius = patrolRadius;
            this.waitTimeAtPoint = waitTimeAtPoint;
            this.pointReachThreshold = pointReachThreshold;
        }
    }
}