using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Utilities
{
    public class BallAutoRotate : MonoBehaviour
    {
        [Header("Rotation Settings: ")]
        [SerializeField] private Vector3 minRotationSpeed = new Vector3(180f, 180f, 0f); // Minimum rotation speed in degrees per second
        [SerializeField] private Vector3 maxRotationSpeed = new Vector3(360f, 360f, 0f); // Maximum rotation speed in degrees per second

        private Vector3 _rotationSpeed;
        
        private bool _isRotating = true;

        private void OnEnable()
        {
            RandomizeRotationSpeed();
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
        
        private void RandomizeRotationSpeed()
        {
            _rotationSpeed = new Vector3(
                Random.Range(minRotationSpeed.x, maxRotationSpeed.x), // Randomize X rotation speed
                Random.Range(minRotationSpeed.y, maxRotationSpeed.y), // Randomize Y rotation speed
                Random.Range(minRotationSpeed.z, maxRotationSpeed.z)  // Randomize Z rotation speed
            );
        }
    }
}