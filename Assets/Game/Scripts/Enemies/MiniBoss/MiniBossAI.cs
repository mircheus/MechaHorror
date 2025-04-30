using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.MiniBoss.States;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss 
{
    public class MiniBossAI : EnemyAI
    {
        [Header("MiniBoss settings:")]
        [SerializeField] private float exitFromRangeAttackRange = 5f;
        
        public float ExitFromRangeAttackRange => exitFromRangeAttackRange;
        
        public override void Init(Dictionary<Type, IState> states)
        {
            base.Init(states);
            StateMachine.Enter<IdleState>();
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