using System;
using System.Collections;
using Game.Scripts.Interfaces;
using Game.Scripts.Player;
using Hertzole.GoldPlayer;
using RetroArsenal;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Utilities
{
    public class Ball : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private GameObject ballGameObject;
        [SerializeField] private SphereCollider sphereCollider;
        [SerializeField] private BoxCollider boxCollider;
        [SerializeField] private ParticleSystem destroyParticlePrefab;
        
        [Header("Rotation Settings: ")]
        [SerializeField] private float rotationStopEnableDelay = 1f; 
        [SerializeField] private Vector3 minRotationSpeed = new Vector3(180f, 180f, 0f); // Minimum rotation speed in degrees per second
        [SerializeField] private Vector3 maxRotationSpeed = new Vector3(360f, 360f, 0f); // Maximum rotation speed in degrees per second

        [Header("Self Destruct Settings: ")]
        [SerializeField] private float selfDestructDelay = 3f; // Time before the ball self-destructs\
        
        private Vector3 _rotationSpeed;
        private Rigidbody _rigidbody;
        private Coroutine _stopRotationCoroutine;
        private bool _isAbleToStopRotating = true;
        private bool _isRotating = true;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            RandomizeRotationSpeed();
            _stopRotationCoroutine = StartCoroutine(EnableStopRotatingWithDelay(rotationStopEnableDelay)); // Stop rotating after 5 seconds
            Destroy(gameObject, selfDestructDelay); // Self-destruct after 10 seconds
        }

        private void OnCollisionEnter(Collision collision)
        {
            // if(collision.collider.TryGetComponent(out PlayerHealth playerHealth))
            // {
            //     playerHealth.TakeDamage(1);
            //     Instantiate(selfDestructParticle, transform.position, Quaternion.identity);
            //     gameObject.SetActive(false);
            // }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Projectile projectile))
            {
                projectile.HitBall(this);
                Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity); // TODO: избавиться от Instantiate
                ballGameObject.SetActive(false);
                DisableColliders();
                // StartCoroutine(SelfDestructWithDelay(selfDestructDelay));
            }
            
            if(other.TryGetComponent(out BallCollider ballCollider))
            {
                gameObject.SetActive(false);
            }

            if (other.TryGetComponent(out Ground ground))
            {
                if (_isAbleToStopRotating)
                {
                    StopRotating();
                }
            }
        }

        private void Update()
        {
            if (_isRotating)
            {
                transform.Rotate(_rotationSpeed * Time.deltaTime);
            }
        }

        public void StopRotating()
        {
            _isRotating = false;
        }

        public void EnableGravity()
        {
            _rigidbody.useGravity = true;
        }
        
        private void RandomizeRotationSpeed()
        {
            _rotationSpeed = new Vector3(
                Random.Range(minRotationSpeed.x, maxRotationSpeed.x), // Randomize X rotation speed
                Random.Range(minRotationSpeed.y, maxRotationSpeed.y), // Randomize Y rotation speed
                Random.Range(minRotationSpeed.z, maxRotationSpeed.z)  // Randomize Z rotation speed
            );
        }

        private IEnumerator EnableStopRotatingWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _isAbleToStopRotating = false;
        }
        
        private IEnumerator SelfDestructWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }

        private void DisableColliders()
        {
            boxCollider.enabled = false;
            sphereCollider.enabled = false;
        }

        // public void TakeDamage(int damage)
        // {
        //     Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity); // TODO: избавиться от Instantiate
        //     ballGameObject.SetActive(false);
        //     DisableColliders();
        // }
    }
}