using System.Collections;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Utilities;
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
        
        [Header("Kick Settings: ")]
        [SerializeField] private float duration = 5f;
        [SerializeField] private float height = 5f;
        
        private Coroutine _kickCoroutine;

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

            if (_kickCoroutine != null)
            {
                StopCoroutine(_kickCoroutine);
            }
            
            _kickCoroutine = StartCoroutine(MoveInArc(ball.transform, ballSpawnPoint.position, target.position, duration, height));
        }
        
        private IEnumerator MoveInArc(Transform obj, Vector3 start, Vector3 end, float duration, float height)
        {
            float elapsed = 0f;

            // XZ displacement
            Vector3 startXZ = new Vector3(start.x, 0, start.z);
            Vector3 endXZ = new Vector3(end.x, 0, end.z);
            Vector3 deltaXZ = endXZ - startXZ;

            float startY = start.y;
            float endY = end.y;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);

                // Base position along flat XZ plane
                Vector3 currentXZ = startXZ + deltaXZ * t;

                // Arc calculation
                float arc = 4 * height * (t - t * t); // parabola with peak at t = 0.5

                // Linear Y base + arc offset
                float currentY = Mathf.Lerp(startY, endY, t) + arc;

                obj.position = new Vector3(currentXZ.x, currentY, currentXZ.z);
                yield return null;
            }

            obj.position = end;
            obj.GetComponent<Ball>().StopRotating();
        }

    }
}