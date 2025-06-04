using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.FourLegEnemy.States;
using UnityEngine;

namespace Game.Scripts.Enemies.FourLegEnemy
{
    public class FourLeg : BaseEnemy
    {
        [SerializeField] private ParticleSystem deathParticle;
        
        protected override Dictionary<Type, IState> GetStates()
        {
            return new Dictionary<Type, IState>
            {
                { typeof(IdleState), new IdleState(enemyAI) },
                { typeof(AttackState), new AttackState(enemyAI) },
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