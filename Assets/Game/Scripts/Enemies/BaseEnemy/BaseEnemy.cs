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
        
        [Header("Base Settings: ")]
        [SerializeField] protected bool manualInit = false;
        
        protected EnemyAI enemyAI;
        protected StateMachine stateMachine;

        public event UnityAction Death;
        public EnemyAI EnemyAI => enemyAI;

        protected abstract void Die();

        protected void Start()
        {
            if (manualInit == false)
            {
                enemyAI = GetComponent<EnemyAI>();
                var states = GetStates();
                enemyAI.Init(states);
                stateMachine = enemyAI.StateMachine;
            }
        }

        public virtual void Init(Transform target)
        {
            enemyAI = GetComponent<EnemyAI>();
            enemyAI.SetTarget(target);
            var states = GetStates();
            enemyAI.Init(states);
            stateMachine = enemyAI.StateMachine;
        }

        public void TakeDamage(int amount)
        {
            // DamagePopUpGenerator.current.CreatePopUpTesting(transform.position + new Vector3(0, 2, 0));
            Debug.Log("TakeDamage");
            health -= amount;
            CheckDeath();
        }

        public void InvokeDeathEvent()
        {
            Death?.Invoke();
        }

        protected virtual Dictionary<Type, IState> GetStates()
        {
            return new Dictionary<Type, IState>
            {
                // { typeof(IdleState), new IdleState(enemyAI) },
                // { typeof(DeadState), new DeadState() }
            };
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
