using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts
{
    [RequireComponent(typeof(EnemyAttack))]
    public class EnemyAI : MonoBehaviour
    {
        public Transform target;
        public float detectionRange = 10f;
        public float attackRange = 9f;
        public float rotationSpeed;
        public float strafeCooldownTime = 2f;

        [HideInInspector] public NavMeshAgent agent;
        [HideInInspector] public StateMachine StateMachine;
        
        private EnemyAttack _enemyAttack;

        public EnemyAttack EnemyAttack => _enemyAttack;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            _enemyAttack = GetComponent<EnemyAttack>();
            StateMachine = new StateMachine();
            StateMachine.ChangeState(new IdleState(this));
        }

        private void Update()
        {
            StateMachine.Update();
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