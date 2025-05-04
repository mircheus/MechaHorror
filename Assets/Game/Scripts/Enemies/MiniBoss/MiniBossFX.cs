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
                dashParticle.gameObject.SetActive(true);
            }
        }
        
        public void DisableDashParticle()
        {
            if (dashParticle != null)
            {
                dashParticle.gameObject.SetActive(false);
            }
        }
    }
}