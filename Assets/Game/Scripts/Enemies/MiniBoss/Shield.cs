using System;
using RetroArsenal;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss.States
{
    [RequireComponent(typeof(BoxCollider))]
    public class Shield : MonoBehaviour
    {
        public event Action ShieldHit;

        public void ActivateShield()
        {
            gameObject.SetActive(true);
        }
        
        public void DeactivateShield()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Projectile projectile))
            {
                ShieldHit?.Invoke();
                projectile.HitShield(this);
            }
        }
    }
}