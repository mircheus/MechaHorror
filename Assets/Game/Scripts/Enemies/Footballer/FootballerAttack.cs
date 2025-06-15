using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.Footballer
{
    public class FootballerAttack : BaseEnemyAttack
    {
        [Header("References: ")]
        [SerializeField] private FootballerAI footballer;
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private Transform ballSpawnPoint;
        [SerializeField] private AnimationEventInvoker animationEventInvoker;
        
        private void OnEnable()
        {
            animationEventInvoker.OnRangeAttack += OnAttack;
        }
        
        private void OnDisable()
        {
            animationEventInvoker.OnRangeAttack -= OnAttack;
        }

        private void OnAttack(AttackType attackType)
        {
            if (attackType == AttackType.BallKickAttack)
            {
                Attack(footballer.Target);
            }
        }
        
        public override void Attack(Transform target)
        {
            var ball = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);
            Debug.Log("Kick");
        }
    }
}