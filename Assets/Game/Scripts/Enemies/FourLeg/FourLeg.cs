using System;
using System.Collections.Generic;
using Game.Scripts.Enemies.FourLeg.States;
using UnityEngine;

namespace Game.Scripts.Enemies.FourLeg
{
    public class FourLeg : BaseEnemy.BaseEnemy
    {
        [Header("FourLeg References: ")]
        [SerializeField] private ParticleSystem deathParticle;
        
        protected override Dictionary<Type, IState> GetStates()
        {
            return new Dictionary<Type, IState>
            {
                { typeof(IdleState), new IdleState(enemyAI) },
                { typeof(AttackState), new AttackState(enemyAI) },
                {typeof(ChaseState), new ChaseState(enemyAI)},
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