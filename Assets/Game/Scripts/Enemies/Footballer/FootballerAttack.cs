using System;
using System.Collections;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.Enemies.Footballer
{
    public class FootballerAttack : BaseEnemyAttack
    {
        [Header("References: ")] [SerializeField]
        private FootballerAI footballer;

        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private Transform ballSpawnPoint;
        [SerializeField] private AnimationEventInvoker animationEventInvoker;

        [Header("Kick Settings: ")] [SerializeField]
        private float duration = 5f;

        [SerializeField] private float height = 5f;
        [SerializeField] private Vector3 offset = new Vector3(0, -14f, 14f);

        [Header("Physics Settings:")] [SerializeField]
        private float force = 10f;

        [SerializeField] private float arcHeight = 2f;

        private Coroutine _kickCoroutine;

        private bool _isAttacking = false;
        // private float _kickTimer;

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
                // KickBall();
            }
        }

        public override void Attack(Transform target)
        {
            var ball = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);

            if (_kickCoroutine != null)
            {
                StopCoroutine(_kickCoroutine);
            }

            var transform1 = transform;
            _kickCoroutine = StartCoroutine(MoveInArc(ball.transform, ballSpawnPoint.position, target.position + (transform1.forward * offset.z) + (transform1.up * offset.y),
                duration, height));
        }

        private IEnumerator MoveInArc(Transform obj, Vector3 start, Vector3 end, float duration, float height)
        {
            var ball = obj.GetComponent<Ball>();
            var rb = obj.GetComponent<Rigidbody>();

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
            ball.StopRotating();
            ball.EnableGravity();
            var forward = transform.forward;
            Vector3 velocity = new Vector3(forward.x, 0, forward.z).normalized * force;
            rb.linearVelocity = velocity;
        }

        private void KickBall()
        {
            GameObject ball = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);

            Rigidbody rb = ball.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("Ball prefab is missing Rigidbody!");
                return;
            }

            Vector3 velocity = CalculateArcVelocity(footballer.Target.position, ballSpawnPoint.position, arcHeight);
            rb.linearVelocity = velocity;
        }

        private Vector3 CalculateArcVelocity(Vector3 target, Vector3 origin, float arcHeight)
        {
            float gravity = Mathf.Abs(Physics.gravity.y);
            Vector3 direction = target - origin;
            Vector3 horizontal = new Vector3(direction.x, 0, direction.z);

            float time = Mathf.Sqrt(-2 * arcHeight / -gravity) + Mathf.Sqrt(2 * (direction.y - arcHeight) / gravity);
            Vector3 velocityY = Vector3.up * Mathf.Sqrt(2 * gravity * arcHeight);
            Vector3 velocityXZ = horizontal / time;

            return velocityXZ + velocityY;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(footballer.Target.position + (transform.forward * 14) + (transform.up * -14), 1f);
        }
    }
}