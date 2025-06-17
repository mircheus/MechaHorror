using CameraShake;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [Header("Health Settings: ")]
        [SerializeField] private int maxHealth = 100;
        
        [Header("Particle Effects: ")]
        [SerializeField] private ParticleSystem damageParticle;
        
        [Header("Screen Shake Settings")]
        [Range(0.1f, 1f)]
        [SerializeField] private float screenShakeStrength = 0.5f;
        [SerializeField] private float frequency = 25f;
        [SerializeField] private int bouncesCount = 5;

        public void TakeDamage(int damage)
        {
            CameraShaker.Presets.ShortShake3D(screenShakeStrength, frequency, bouncesCount);
            damageParticle.Play();
        }
    }

    public interface IDamageable
    {
        void TakeDamage(int damage);
    }
}
