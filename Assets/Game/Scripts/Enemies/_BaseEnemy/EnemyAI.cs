using System;
using System.Collections.Generic;
using Game.Scripts.Enemies.FourLegEnemy.States;
using UnityEditor;
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

        [Header("Gizmos Settings:")] 
        [SerializeField] private bool isGizmosEnabled = true;

        protected NavMeshAgent agent;
        protected StateMachine stateMachine;
        protected BaseEnemyAttack baseEnemyAttack;

        public BaseEnemyAttack BaseEnemyAttack => baseEnemyAttack;
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
            baseEnemyAttack = GetComponent<BaseEnemyAttack>();
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
            if (isGizmosEnabled)
            {
                var position = transform.position;
                Handles.color = new Color(0, 0, 1, .5f);
                Handles.DrawSolidDisc(position, Vector3.up, detectionRange);
                Handles.color = new Color(1, 0, 0, .5f);
                Handles.DrawSolidDisc(position, Vector3.up, attackRange);
            }
        }
    }
}