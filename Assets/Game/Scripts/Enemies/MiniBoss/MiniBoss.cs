using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.MiniBoss.States;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss
{
    public class MiniBoss : BaseEnemy
    {
        [Header("References:")]
        [SerializeField] private Animator animator;
        [SerializeField] private MiniBossFX miniBossFX;
        [SerializeField] private Shield shield;

        [Header("Weapon Objects:")]
        [SerializeField] private bool isSwordEnabled;
        [SerializeField] private GameObject sword;
        
        private new void Start()
        {
            base.Start();
            
            if (isSwordEnabled)
            {
                sword.SetActive(true);
            }
        }

        protected override Dictionary<Type, IState> GetStates()
        {
            return new Dictionary<Type, IState>
            {
                { typeof(IdleState), new IdleState(this, (MiniBossAI)enemyAI, animator) },
                { typeof(RangeAttackState), new RangeAttackState((MiniBossAI)enemyAI, animator) },
                { typeof(ChaseState), new ChaseState((MiniBossAI)enemyAI, animator) },
                { typeof(ShootState), new ShootState(this, (MiniBossAI)enemyAI, animator) },
                { typeof(DashState), new DashState(this, (MiniBossAI)enemyAI, animator, miniBossFX) },
                { typeof(ShieldState), new ShieldState((MiniBossAI)enemyAI, shield) }
            };
        }
        
        public void RotateTowardTargetAroundY()
        {
            var direction = (enemyAI.Target.position - enemyAI.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            lookRotation.x = 0; // Keep the x rotation at 0
            lookRotation.z = 0; // Keep the z rotation at 0
            enemyAI.transform.rotation = Quaternion.Slerp(enemyAI.transform.rotation, lookRotation,
                Time.deltaTime * enemyAI.RotationSpeed);
        }
    }
}
