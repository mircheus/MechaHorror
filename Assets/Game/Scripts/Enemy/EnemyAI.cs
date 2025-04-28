using UnityEngine;
using UnityEngine.AI;

namespace Game.Scripts
{
    [RequireComponent(typeof(EnemyAttack))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float detectionRange = 10f;
        [SerializeField] private float attackRange = 9f;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float strafeCooldownTime = 2f;

        private NavMeshAgent _agent;
        private StateMachine _stateMachine;
        private EnemyAttack _enemyAttack;

        public EnemyAttack EnemyAttack => _enemyAttack;
        public StateMachine StateMachine => _stateMachine;
        public NavMeshAgent Agent => _agent;
        public Transform Target => target;
        public float DetectionRange => detectionRange;
        public float AttackRange => attackRange;
        public float RotationSpeed => rotationSpeed;
        public float StrafeCooldownTime => strafeCooldownTime;

        public void Init()
        {
            _agent = GetComponent<NavMeshAgent>();
            _enemyAttack = GetComponent<EnemyAttack>();
            _stateMachine = new StateMachine();
            _stateMachine.ChangeState(new IdleState(this));
        }

        private void Update()
        {
            _stateMachine.Update();
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