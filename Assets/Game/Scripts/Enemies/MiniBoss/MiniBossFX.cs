using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss
{
    public class MiniBossFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem dashParticle;
        
        public void EnableDashParticle()
        {
            if (dashParticle != null)
            {
                dashParticle.Play();
            }
        }
        
        public void DisableDashParticle()
        {
            if (dashParticle != null)
            {
                dashParticle.Stop();
            }
        }
    }
}