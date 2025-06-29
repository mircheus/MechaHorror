using System;
using System.Collections.Generic;
using Game.Scripts.Enemies.Footballer.States;
using UnityEngine;
using AttackState = Game.Scripts.Enemies.Footballer.States.AttackState;
using DeadState = Game.Scripts.Enemies.Footballer.States.DeadState;

namespace Game.Scripts.Enemies.Footballer
{
    public class Footballer : BaseEnemy.BaseEnemy
    {
        [Header("Footballer References: ")]
        [SerializeField] private Animator animator;
        [SerializeField] private ParticleSystem deathParticle;
        [SerializeField] private GameObject mechGameObject;
        [SerializeField] private BoxCollider mainCollider;

        protected override Dictionary<Type, IState> GetStates()
        {
            return new Dictionary<Type, IState>
            {
                {typeof(IdleState), new IdleState(enemyAI, animator)},
                {typeof(AttackState), new AttackState(enemyAI, animator)},
                {typeof(DeadState), new DeadState(enemyAI, deathParticle, mechGameObject, mainCollider) },
                { typeof(ChaseState), new ChaseState(enemyAI, animator) }
            };
        }

        protected override void Die()
        {
            stateMachine.Enter<DeadState>();
        }
    }
}