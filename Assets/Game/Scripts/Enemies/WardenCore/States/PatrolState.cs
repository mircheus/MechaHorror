using System.Linq;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.WardenCore.States.StateData;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.Enemies.WardenCore.States
{
    public class PatrolState : IState
    {
        private readonly Transform[] _patrolPoints;
        private Transform _playerPosition;
        private float _patrolRadius = 10f;
        private float _waitTimeAtPoint = 2f;
        private float _pointReachThreshold = 0.5f;
        public int _numberOfClosestPointsToPickFrom = 3;
        
        private EnemyAI _enemyAI;
        private NavMeshAgent _agent;
        private Vector3 _targetPoint;
        private float _waitTimer = 0f;
        private bool _waiting = false;
        private int _currentPointIndex = -1;

        public PatrolState(EnemyAI enemyAI, PatrolSettings patrolSettings)
        {
            _enemyAI = enemyAI;
            _agent = enemyAI.Agent;
            _patrolRadius = patrolSettings.patrolRadius;
            _waitTimeAtPoint = patrolSettings.waitTimeAtPoint;
            _pointReachThreshold = patrolSettings.pointReachThreshold;
            _patrolPoints = patrolSettings.patrolPoints;
        }
        
        public void Enter()
        {
            SetFirstPoint();
        }

        public void Execute()
        {
            // PatrolPoints();
            // RadiusPatrol();
            PatrolPointsNearestToPlayer();
        }

        public void Exit()
        {

        }

        private void PatrolPoints() // TODO: Объединить два метода(PatrolPoints, PatrolPointsNearestToPlayer)  в один с чередованием способа выбора следующей точки
        {
            if (_patrolPoints.Length == 0) return;

            if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_waiting)
                {
                    _waiting = true;
                    _waitTimer = 0f;
                }

                _waitTimer += Time.deltaTime;

                if (_waitTimer >= _waitTimeAtPoint)
                {
                    GoToNextPoint();
                    // var random = Random.Range(0, 2);
                    //
                    // if (random == 0)
                    // {
                    //     GoToNextPoint();
                    // }
                    // else
                    // {
                    //     GoToClosestToPlayerPoint();
                    // }
                    
                    _waiting = false;
                }
            }
        }
        
        private void PatrolPointsNearestToPlayer()
        {
            if (_patrolPoints.Length == 0) return;

            if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_waiting)
                {
                    _waiting = true;
                    _waitTimer = 0f;
                }

                _waitTimer += Time.deltaTime;

                if (_waitTimer >= _waitTimeAtPoint)
                {
                    GoToClosestToPlayerPoint();
                    _waiting = false;
                }
            }
        }
        
        private void SetFirstPoint()
        {
            if (_patrolPoints.Length > 0)
            {
                _agent.SetDestination(_patrolPoints[0].position);
            }
            else
            {
                Debug.LogWarning("No patrol points assigned to EnemyPatrol");
            }
        }
        
        private void GoToNextPoint()
        {
            SetNextPoint(true);
            _agent.SetDestination(_patrolPoints[_currentPointIndex].position);
        }
        
        private void GoToClosestToPlayerPoint()
        {
            // Sort patrol points by distance to player
            Transform[] sortedPoints = _patrolPoints.OrderBy(p => Vector3.Distance(p.position, _enemyAI.Target.position)).ToArray();

            // Pick randomly among the N closest
            int pickLimit = Mathf.Min(_numberOfClosestPointsToPickFrom, sortedPoints.Length);
            int randomIndex = Random.Range(1, pickLimit);

            _agent.SetDestination(sortedPoints[randomIndex].position);
        }

        private void SetNextPoint(bool isRandom)
        {
            if (isRandom)
            {
                int index = -1;
                
                while (index == -1 || index == _currentPointIndex)
                {
                    index = Random.Range(0, _patrolPoints.Length);
                }
                
                _currentPointIndex = index;
            }
            else
            {
                _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
            }
        }
    }
}