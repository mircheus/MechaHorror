using System;
using CameraShake;
using Game.Scripts.Interfaces;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [Header("References: ")]
        [SerializeField] private BallCollider ballCollider;
        
        [Header("Health Settings: ")]
        [SerializeField] private int maxHealth = 100;
        
        [Header("Particle Effects: ")]
        [SerializeField] private ParticleSystem damageParticle;
        
        [Header("Screen Shake Settings")]
        [Range(0.1f, 1f)]
        [SerializeField] private float screenShakeStrength = 0.5f;
        [SerializeField] private float frequency = 25f;
        [SerializeField] private int bouncesCount = 5;

        private void OnEnable()
        {
            ballCollider.BallCollision += OnBallCollision;
        }
        
        private void OnDisable()
        {
            ballCollider.BallCollision -= OnBallCollision;
        }

        public void TakeDamage(int damage)
        {
            maxHealth -= damage;
            CameraShaker.Presets.ShortShake3D(screenShakeStrength, frequency, bouncesCount);
            damageParticle.Play();
        }

        private void OnBallCollision()
        {
            TakeDamage(1);
        }
    }
}
