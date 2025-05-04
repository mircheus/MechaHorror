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
        [Header("References: ")]
        [SerializeField] private MiniBossAttack miniBossAttack;
        
        [Header("MiniBoss settings:")]
        [SerializeField] private bool isRangeAttackDirectionForward = true;
        
        [Header("Shield:")]
        [SerializeField] private bool isTimeBased = false;
        [SerializeField] private float shieldDuration = 5f;
        [SerializeField] private bool isDamageBased = false;
        [SerializeField] private int damageThreshold = 10;
        
        [Header("Dash:")]
        [SerializeField] private ProjectileCollider projectileCollider;
        [SerializeField] private float dashDistance = 20f;
        [SerializeField] private float dashDuration = 1f;
        
        private bool _isDashAllowed;

        public bool IsRangeAttackDirectionForward => isRangeAttackDirectionForward;
        public float DashDistance => dashDistance;
        public float DashDuration => dashDuration;
        public bool IsTimeBased => isTimeBased;
        public float ShieldDuration => shieldDuration;
        public bool IsDamageBased => isDamageBased;
        public int DamageThreshold => damageThreshold;

        private void OnEnable()
        {
            miniBossAttack.RangeAttackPerformed += OnRangeAttackPerformed;
            
            if (projectileCollider != null)
            {
                projectileCollider.ProjectileTriggerEnter += OnProjectileTriggerEnter;
            }
        }

        private void OnDisable()
        {
            miniBossAttack.RangeAttackPerformed -= OnRangeAttackPerformed;
            
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

        protected override void GizmosMethods()
        {
            var position = transform.position;
            Handles.color = new Color(0, 0, 1, .5f);
            Handles.DrawSolidDisc(position, Vector3.up, detectionRange);
        }

        private void OnProjectileTriggerEnter()
        {
            if (_isDashAllowed)
            {
                stateMachine.Enter<DashState>();
                _isDashAllowed = false;
            }
        }

        private void OnRangeAttackPerformed()
        {
            _isDashAllowed = true;
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