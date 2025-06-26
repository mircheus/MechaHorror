using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.FourLeg.States;
using UnityEngine;

namespace Game.Scripts.Enemies.FourLegEnemy
{
    public class FourLeg : BaseEnemy.BaseEnemy
    {
        [Header("FourLeg References: ")]
        [SerializeField] private ParticleSystem deathParticle;
        [SerializeField] private float strafeDistanceMin;
        [SerializeField] private float strafeDistanceMax;
        [SerializeField] private EnemySight enemySight;

        [Header("Attack State settings: ")] 
        [SerializeField] private bool isStrafeEnabled;
        
        protected override Dictionary<Type, IState> GetStates()
        {
            return new Dictionary<Type, IState>
            {
                { typeof(IdleState), new IdleState(enemyAI) },
                { typeof(AttackState), new AttackState(enemyAI, enemyAI.BaseEnemyAttack, isStrafeEnabled, strafeDistanceMin, strafeDistanceMax) },
                {typeof(ChaseState), new ChaseState(enemyAI, enemySight)},
                { typeof(DeadState), new DeadState(this, deathParticle) }
            };
        }

        protected override void Die()
        {
            base.Die();
            stateMachine.Enter<DeadState>();
        }
    }
}