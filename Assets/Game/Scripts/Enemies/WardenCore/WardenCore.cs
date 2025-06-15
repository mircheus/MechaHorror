using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.FourLegEnemy.States;
using Game.Scripts.Enemies.WardenCore.States;
using Game.Scripts.Enemies.WardenCore.States.StateData;
using UnityEngine;
using ChaseState = Game.Scripts.Enemies.WardenCore.States.ChaseState;

namespace Game.Scripts.Enemies.WardenCore
{
    public class WardenCore : BaseEnemy.BaseEnemy
    {
        [Header("Patrol Settings")] 
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private float patrolRadius = 10f;
        [SerializeField] private float waitTimeAtPoint = 2f;
        [SerializeField] private float pointReachThreshold = 0.5f;
        
        protected override Dictionary<Type, IState> GetStates()
        {
            return new Dictionary<Type, IState>
            {
                // { typeof(IdleState), new IdleState(enemyAI) },
                { typeof(ChaseState), new ChaseState(enemyAI) },
                { typeof(PatrolState), new PatrolState(enemyAI, new PatrolSettings(patrolPoints, patrolRadius, waitTimeAtPoint, pointReachThreshold)) },
                // { typeof(AttackState), new AttackState(enemyAI) },
            };
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, patrolRadius);
        }
    }
}