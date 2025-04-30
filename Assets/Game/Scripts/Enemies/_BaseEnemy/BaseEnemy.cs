using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Enemies._BaseEnemy
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField] protected int health = 3;
        [SerializeField] protected EnemyAI enemyAI;
    
        protected StateMachine stateMachine;

        protected void Start()
        {
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
