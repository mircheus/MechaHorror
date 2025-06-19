using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Enemies.BaseEnemy
{
    [RequireComponent(typeof(EnemyAI))]
    public abstract class BaseEnemy : MonoBehaviour
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
            // DamagePopUpGenerator.current.CreatePopUp(amount.ToString(), true);
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
