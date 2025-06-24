using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.Kamikaje.States;
using UnityEngine;

namespace Game.Scripts.Enemies.Kamikaje
{
    public class Kamikaje : BaseEnemy.BaseEnemy
    {
        [Header("Kamikaje References: ")] 
        [SerializeField] private GameObject mechGameObject;
        [SerializeField] private Animator animator;
        [SerializeField] private ParticleSystem deathParticle;
        [SerializeField] private SphereCollider explosionCollider;
        [SerializeField] private BoxCollider mainCollider;
        [SerializeField] private KamikajeTrigger kamikajeTrigger;
        
        protected override Dictionary<Type, IState> GetStates()
        {
            return new Dictionary<Type, IState>
            {
                { typeof(ChasingState), new ChasingState(enemyAI, animator, kamikajeTrigger) },
                { typeof(AttackState), new AttackState(enemyAI, deathParticle, explosionCollider) },
                { typeof(DeadState), new DeadState(enemyAI, deathParticle, mechGameObject, mainCollider) }
            };
        }

        protected override void Die()
        {
            base.Die();
            stateMachine.Enter<DeadState>();
        }
    }
}