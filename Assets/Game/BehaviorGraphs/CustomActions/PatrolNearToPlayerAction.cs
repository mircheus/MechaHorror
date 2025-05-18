using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PatrolNearToPlayer", story: "[Agent] patrols nearest [Waypoints] to [Player]", category: "Action", id: "4167a6a5b03063ca2f4f7896514b6b4a")]
public partial class PatrolNearToPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<List<GameObject>> Waypoints;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    [SerializeReference] public BlackboardVariable<int> numberOfClosestPointsToPickFrom = new BlackboardVariable<int>(3);
    [SerializeReference] public BlackboardVariable<float> Speed;
    [SerializeReference] public BlackboardVariable<float> WaypointWaitTime = new BlackboardVariable<float>(1.0f);
    [SerializeReference] public BlackboardVariable<float> DistanceThreshold = new BlackboardVariable<float>(0.2f);
    [SerializeReference] public BlackboardVariable<string> AnimatorSpeedParam = new BlackboardVariable<string>("SpeedMagnitude");
    [Tooltip("Should patrol restart from the latest point?")]
    [SerializeReference] public BlackboardVariable<bool> PreserveLatestPatrolPoint = new (false);
    [Tooltip("Should exclude the most nearest point to Player?")]
    [SerializeReference] public BlackboardVariable<bool> ExcludeMostNearestPoint = new (true);

    private NavMeshAgent m_NavMeshAgent;
    // private Animator m_Animator;
    private float m_PreviousStoppingDistance;
        
    [CreateProperty]
    private Vector3 m_CurrentTarget;
    [CreateProperty]
    private int m_CurrentPatrolPoint = 0;
    [CreateProperty]
    private bool m_Waiting;
    [CreateProperty]
    private float m_WaypointWaitTimer;
    
    protected override Status OnStart()
    {
        if (Agent.Value == null)
        {
            LogFailure("No agent assigned.");
            return Status.Failure;
        }

        if (Waypoints.Value == null || Waypoints.Value.Count == 0)
        {
            LogFailure("No waypoints to patrol assigned.");
            return Status.Failure;
        }
        
        Initialize();

        m_Waiting = false;
        m_WaypointWaitTimer = 0.0f;

        MoveToNextWaypoint();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Agent.Value == null || Waypoints.Value == null)
        {
            return Status.Failure;
        }

        if (m_Waiting)
        {
            if (m_WaypointWaitTimer > 0.0f)
            {
                m_WaypointWaitTimer -= Time.deltaTime;
            }
            else
            {
                m_WaypointWaitTimer = 0f;
                m_Waiting = false;
                MoveToNextWaypoint();
            }
        }
        else
        {
            float distance = GetDistanceToWaypoint();
            Vector3 agentPosition = Agent.Value.transform.position;
                
            // If we are using navmesh, get the animator speed out of the velocity.
            // if (m_Animator != null && m_NavMeshAgent != null)
            // {
            //     m_Animator.SetFloat(AnimatorSpeedParam, m_NavMeshAgent.velocity.magnitude);
            // }

            if (distance <= DistanceThreshold)
            {
                // if (m_Animator != null)
                // {
                //     m_Animator.SetFloat(AnimatorSpeedParam, 0);
                // }

                m_WaypointWaitTimer = WaypointWaitTime.Value;
                m_Waiting = true;
            }
            else if (m_NavMeshAgent == null)
            {
                float speed = Mathf.Min(Speed, distance);

                Vector3 toDestination = m_CurrentTarget - agentPosition;
                toDestination.y = 0.0f;
                toDestination.Normalize();
                agentPosition += toDestination * (speed * Time.deltaTime);
                Agent.Value.transform.position = agentPosition;
                Agent.Value.transform.forward = toDestination;
            }
        }

        return Status.Running;
    }

    protected override void OnEnd()
    {
        // if (m_Animator != null)
        // {
        //     m_Animator.SetFloat(AnimatorSpeedParam, 0);
        // }

        if (m_NavMeshAgent != null)
        {
            if (m_NavMeshAgent.isOnNavMesh)
            {
                m_NavMeshAgent.ResetPath();
            }
            m_NavMeshAgent.stoppingDistance = m_PreviousStoppingDistance;
        }
    }
    
    protected override void OnDeserialize()
    {
        Initialize();
    }

    private void Initialize()
    {
        // m_Animator = Agent.Value.GetComponentInChildren<Animator>();
        // if (m_Animator != null)
        // {
        //     m_Animator.SetFloat(AnimatorSpeedParam, 0);
        // }

        m_NavMeshAgent = Agent.Value.GetComponentInChildren<NavMeshAgent>();
        if (m_NavMeshAgent != null)
        {
            if (m_NavMeshAgent.isOnNavMesh)
            {
                m_NavMeshAgent.ResetPath();
            }
            m_NavMeshAgent.speed = Speed.Value;
            m_PreviousStoppingDistance = m_NavMeshAgent.stoppingDistance;
            m_NavMeshAgent.stoppingDistance = DistanceThreshold;
        }

        // m_CurrentPatrolPoint = PreserveLatestPatrolPoint.Value ? m_CurrentPatrolPoint - 1 : -1;
        // m_CurrentPatrolPoint = 1;
    }

    private float GetDistanceToWaypoint()
    {
        if (m_NavMeshAgent != null)
        {
            return m_NavMeshAgent.remainingDistance;
        }

        Vector3 targetPosition = m_CurrentTarget;
        Vector3 agentPosition = Agent.Value.transform.position;
        agentPosition.y = targetPosition.y; // Ignore y for distance check.
        return Vector3.Distance(
            agentPosition,
            targetPosition
        );
    }

    private void MoveToNextWaypoint()
    {
        m_CurrentPatrolPoint = GetClosestToPlayerPoint();
        // m_CurrentPatrolPoint = SetRandomNextPoint();
        // m_CurrentPatrolPoint = (m_CurrentPatrolPoint + 1) % Waypoints.Value.Count;            

        m_CurrentTarget = Waypoints.Value[m_CurrentPatrolPoint].transform.position;
        // Debug.Log("CurrentPatrolPoint: " + m_CurrentPatrolPoint + " - Waypoint: " + Waypoints.Value[m_CurrentPatrolPoint].transform.position );
        if (m_NavMeshAgent != null)
        {
            m_NavMeshAgent.SetDestination(m_CurrentTarget);
        }
        // else if (m_Animator != null)
        // {
        //     // We set the animator speed once if we are using transform.
        //     m_Animator.SetFloat(AnimatorSpeedParam, Speed.Value);
        // }
    }

    private int SetRandomNextPoint()
    {
        int index = -1;
                
        while (index == -1 || index == m_CurrentPatrolPoint)
        {
            index = Random.Range(0, Waypoints.Value.Count);
        }
                
        return index;
    }
    
    private int GetClosestToPlayerPoint()
    {
        // Sort patrol points by distance to player
        var playerPosition = Player.Value.transform.position;
        // Debug.Log("playerPosition: " + playerPosition);
        GameObject[] sortedPoints = Waypoints.Value.OrderBy(p => Vector3.Distance(p.transform.position, playerPosition)).ToArray();
        // Debug.Log("Index of nearest point: " + Waypoints.Value.IndexOf(sortedPoints[0]) + " distance: " + Vector3.Distance(sortedPoints[0].transform.position, playerPosition) + " to player." + " - " + sortedPoints[0].transform.position + " - Player: " + Player.Value.transform.position + " - Waypoint: " + sortedPoints[0].transform.position);
        DebugStackOf(sortedPoints);
        // Pick randomly among the N closest
        // int pickLimit = Mathf.Min(numberOfClosestPointsToPickFrom, sortedPoints.Length);
        int randomIndex = Random.Range(ExcludeMostNearestPoint ? 1 : 0, numberOfClosestPointsToPickFrom);
        int pickIndex = Waypoints.Value.IndexOf(sortedPoints[randomIndex]);
        // Debug.Log("PickIndex: " + pickIndex + " - " + sortedPoints[randomIndex].transform.position + " - Player: " + Player.Value.transform.position + " - Waypoint: " + sortedPoints[randomIndex].transform.position + " - Distance: " + Vector3.Distance(sortedPoints[randomIndex].transform.position, playerPosition) + " to player.");
        return pickIndex;
        // m_NavMeshAgent.SetDestination(sortedPoints[randomIndex].transform.position);
    }

    private void DebugStackOf(GameObject[] waypoints)
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            // Debug.Log("Waypoint " + i + ": " + waypoints[i].transform.position + " - Distance: " + Vector3.Distance(waypoints[i].transform.position, Player.Value.transform.position) + " to player.");
        }
    }
}

