using System;
using System.Collections;
using Game.Scripts.Player;
using Hertzole.GoldPlayer;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Utilities
{
    public class Ball : MonoBehaviour
    {
        private const string Ground = "Ground";
        
        [Header("Rotation Settings: ")]
        [SerializeField] private float rotationStopDelay = 2f; 
        [SerializeField] private Vector3 minRotationSpeed = new Vector3(180f, 180f, 0f); // Minimum rotation speed in degrees per second
        [SerializeField] private Vector3 maxRotationSpeed = new Vector3(360f, 360f, 0f); // Maximum rotation speed in degrees per second

        [Header("Self Destruct Settings: ")]
        [SerializeField] private float selfDestructDelay = 10f; // Time before the ball self-destructs\

        private Vector3 _rotationSpeed;
        private Rigidbody _rigidbody;
        private Coroutine _stopRotationCoroutine;
        private bool _isRotating = true;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            RandomizeRotationSpeed();
            // _stopRotationCoroutine = StartCoroutine(StopRotatingWithDelay(rotationStopDelay)); // Stop rotating after 5 seconds
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
            if(other.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(1);
                gameObject.SetActive(false);
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

        private IEnumerator StopRotatingWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _isRotating = false;
        }
    }
}