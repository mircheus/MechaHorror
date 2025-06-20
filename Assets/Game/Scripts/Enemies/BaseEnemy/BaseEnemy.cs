using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Interfaces;
using Game.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Enemies.BaseEnemy
{
    [RequireComponent(typeof(EnemyAI))]
    public abstract class BaseEnemy : MonoBehaviour, IDamageable
    {
        [Header("Base References: ")]
        [SerializeField] protected int health = 3;
        
        protected EnemyAI enemyAI;
        protected StateMachine stateMachine;

        public event UnityAction Death;

        protected void Start()
        {
            enemyAI = GetComponent<EnemyAI>();
            var states = GetStates();
            enemyAI.Init(states);
            stateMachine = enemyAI.StateMachine;
        }

        public void TakeDamage(int amount)
        {
            // DamagePopUpGenerator.current.CreatePopUpTesting(transform.position + new Vector3(0, 2, 0));
            health -= amount;
            CheckDeath();
        }

        protected virtual Dictionary<Type, IState> GetStates()
        {
            return new Dictionary<Type, IState>
            {
                // { typeof(IdleState), new IdleState(enemyAI) },
                // { typeof(DeadState), new DeadState() }
            };
        }

        protected virtual void Die()
        {
            Death?.Invoke();
            // stateMachine.ChangeState(new DeadState());
            // stateMachine.Enter<DeadState>();
            // _meshRenderer.enabled = false;
            // deathParticle.SetActive(true);
        }

        private void CheckDeath()
        {
            if(health <= 0)
            {
                Die();
            }
        }
    }
}
