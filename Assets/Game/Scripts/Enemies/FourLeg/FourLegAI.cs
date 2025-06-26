using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.FourLeg.States;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Enemies.FourLeg
{
    public class FourLegAI : EnemyAI
    {
        [Header("Gizmos Settings: ")]
        [SerializeField] private bool showDetectionRange = true;
        [SerializeField] private bool showAttackRange = true;
        [SerializeField] private bool showStoppingDistance = true;
        
        public override void Init(Dictionary<Type, IState> states)
        {
            base.Init(states);
            stateMachine.Enter<IdleState>();
        }

        protected override void GizmosMethods()
        {
            var position = transform.position;

            if (showDetectionRange)
            {
                Handles.color = new Color(0, 0, 1, .5f);
                Handles.DrawSolidDisc(position, Vector3.up, detectionRange);
            }

            if (showAttackRange)
            {
                Handles.color = new Color(1, 0, 0, .5f);
                Handles.DrawSolidDisc(position, Vector3.up, attackRange);
            }

            if (showStoppingDistance)
            {
                Handles.color = Color.green;
                Handles.DrawSolidDisc(position, Vector3.up, agent.stoppingDistance);
            }
        }
    }
}