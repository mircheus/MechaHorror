using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.WardenCore.States;
using UnityEngine;

namespace Game.Scripts.Enemies.WardenCore
{
    public class WardenCoreAI : EnemyAI
    {
        public override void Init(Dictionary<Type, IState> states)
        {
            base.Init(states);
            StateMachine.Enter<PatrolState>();
        }

        protected override void GizmosMethods()
        {
            // base.GizmosMethods();
            var value = agent.stoppingDistance;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, value);
        }

        private void HearPlayer()
        {
            
        }
    }
}