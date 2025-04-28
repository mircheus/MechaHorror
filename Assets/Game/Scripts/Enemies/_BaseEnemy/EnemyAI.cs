using System;
using System.Collections.Generic;
using Game.Scripts.Enemies.FourLegEnemy.States;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts.Enemies._BaseEnemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float detectionRange = 10f;
        [SerializeField] private float attackRange = 9f;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float strafeCooldownTime = 2f;   

        protected NavMeshAgent agent;
        protected StateMachine stateMachine;
        protected EnemyAttackBase enemyAttackBase;

        public EnemyAttackBase EnemyAttackBase => enemyAttackBase;
        public StateMachine StateMachine => stateMachine;
        public NavMeshAgent Agent => agent;
        public Transform Target => target;
        public float DetectionRange => detectionRange;
        public float AttackRange => attackRange;
        public float RotationSpeed => rotationSpeed;
        public float StrafeCooldownTime => strafeCooldownTime;

        public void Init(Dictionary<Type, IState> states)
        {
            agent = GetComponent<NavMeshAgent>();
            enemyAttackBase = GetComponent<EnemyAttackBase>();
            stateMachine = new StateMachine(this, states);
            // stateMachine.ChangeState(new IdleState(this));
            stateMachine.Enter<IdleState>();
        }
        
        private void Update()
        {
            stateMachine.Update();
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}