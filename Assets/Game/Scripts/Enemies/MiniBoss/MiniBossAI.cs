using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.MiniBoss.States;
using RetroArsenal;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss 
{
    public class MiniBossAI : EnemyAI
    {
        [Header("MiniBoss settings:")]
        [SerializeField] private bool isRangeAttackDirectionForward = true;

        [Header("Dash:")]
        [SerializeField] private ProjectileCollider projectileCollider;
        [SerializeField] private float dashDistance = 20f;
        [SerializeField] private float dashDuration = 1f;
        
        public bool IsRangeAttackDirectionForward => isRangeAttackDirectionForward;
        public float DashDistance => dashDistance;
        public float DashDuration => dashDuration;

        private void OnEnable()
        {
            if (projectileCollider != null)
            {
                projectileCollider.ProjectileTriggerEnter += OnProjectileTriggerEnter;
            }
        }
        
        private void OnDisable()
        {
            if (projectileCollider != null)
            {
                projectileCollider.ProjectileTriggerEnter -= OnProjectileTriggerEnter;
            }
        }
        
        public override void Init(Dictionary<Type, IState> states)
        {
            base.Init(states);
            StateMachine.Enter<IdleState>();
        }

        private void OnProjectileTriggerEnter()
        {
            Debug.Log("EnterDashState");
            stateMachine.Enter<DashState>();
        }


        // public void RotateTowardTargetAroundY()
        // {
        //     Vector3 direction;
        //     
        //     if (IsRangeAttackDirectionForward)
        //     {
        //         direction = transform.forward;
        //     }
        //     else
        //     {
        //         direction = (Target.position - transform.position).normalized;
        //     }
        //     
        //     Quaternion lookRotation = Quaternion.LookRotation(direction);
        //     lookRotation.x = 0; // Keep the x rotation at 0
        //     lookRotation.z = 0; // Keep the z rotation at 0
        //     transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,
        //         Time.deltaTime * RotationSpeed);
        // }
    }
}