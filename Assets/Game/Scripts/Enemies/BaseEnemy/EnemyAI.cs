using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Game.Scripts.Enemies._BaseEnemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class EnemyAI : MonoBehaviour
    {
        [Header("Base References:")]
        [SerializeField] protected BaseEnemyAttack baseEnemyAttack;
        [SerializeField] protected NavMeshAgent agent;
        [SerializeField] protected Transform target;
        [SerializeField] protected float detectionRange = 10f;
        [SerializeField] protected float attackRange = 9f;
        [SerializeField] protected float rotationSpeed;
        [SerializeField] protected float strafeCooldownTime = 2f;

        [Header("Gizmos:")] 
        [SerializeField] protected bool isGizmosEnabled = true;
        
        protected StateMachine stateMachine;

        public BaseEnemyAttack BaseEnemyAttack => baseEnemyAttack;
        public StateMachine StateMachine => stateMachine;
        public NavMeshAgent Agent => agent;
        public Transform Target => target;
        public float DetectionRange => detectionRange;
        public float AttackRange => attackRange;
        public float RotationSpeed => rotationSpeed;
        public float StrafeCooldownTime => strafeCooldownTime;
        
        public event UnityAction PlayerDetected;
        
        public virtual void Init(Dictionary<Type, IState> states)
        {
            agent = GetComponent<NavMeshAgent>();
            // baseEnemyAttack = GetComponent<BaseEnemyAttack>();
            stateMachine = new StateMachine(this, states);
            // stateMachine.ChangeState(new IdleState(this));
            // stateMachine.Enter<IdleState>();
        }
        
        protected virtual void Update()
        {
            stateMachine.Update();
        }

        protected virtual void GizmosMethods()
        {
            var position = transform.position;
            Handles.color = new Color(0, 0, 1, .5f);
            Handles.DrawSolidDisc(position, Vector3.up, detectionRange);
            Handles.color = new Color(1, 0, 0, .5f);
            Handles.DrawSolidDisc(position, Vector3.up, attackRange);
            Handles.color = Color.green;
            Handles.DrawSolidDisc(position, Vector3.up, agent.stoppingDistance);
        }

        public void OnDrawGizmos()
        {
            if (isGizmosEnabled)
            {
                GizmosMethods();
            }
        }
        
        public void InvokePlayerDetected()
        {
            PlayerDetected?.Invoke();
        }

        public virtual void GetTriggeredByAlarm(AlarmTrigger alarmTrigger)
        {
            // This method can be overridden by derived classes to handle alarm triggers
            Debug.Log($"EnemyAI triggered by alarm: {alarmTrigger.name}");
        }
        
        public void SetTarget(Transform targetTransform)
        {
            if (targetTransform == null)
            {
                Debug.LogWarning($"Target is null. Cannot set target for EnemyAI of {gameObject.name}.");
                return;
            }
            
            target = targetTransform;
        }
    }
}