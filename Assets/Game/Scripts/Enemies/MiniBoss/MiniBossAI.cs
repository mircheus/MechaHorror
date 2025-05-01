using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.MiniBoss.States;
using RetroArsenal;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss 
{
    public class MiniBossAI : EnemyAI
    {
        [Header("MiniBoss settings:")]
        [SerializeField] private float exitFromRangeAttackRange = 5f;
        [SerializeField] private bool isRangeAttackDirectionForward = true;
        
        [Header("Strafe:")]
        [SerializeField] private float dashDistance = 20f;
        [SerializeField] private float dashDuration = 1f;
        
        public float ExitFromRangeAttackRange => exitFromRangeAttackRange;
        public bool IsRangeAttackDirectionForward => isRangeAttackDirectionForward;
        public float DashDistance => dashDistance;
        public float DashDuration => dashDuration;
        
        public override void Init(Dictionary<Type, IState> states)
        {
            base.Init(states);
            StateMachine.Enter<IdleState>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Projectile projectile))
            {
                Debug.Log("Projectile Triggered");
                StateMachine.Enter<DashState>();
            }
        }

        private new void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            if(isGizmosEnabled == false) return;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, exitFromRangeAttackRange);
        }
    }
}