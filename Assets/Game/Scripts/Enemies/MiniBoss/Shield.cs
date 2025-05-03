using System;
using RetroArsenal;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss.States
{
    [RequireComponent(typeof(BoxCollider))]
    public class Shield : MonoBehaviour
    {
        [SerializeField] private Material[] damagedMaterials;
        [SerializeField] private MeshRenderer meshRenderer;

        private int _currentMatIndex = 0;

        public event Action ShieldHit;

        public void ActivateShield()
        {
            _currentMatIndex = 0;
            meshRenderer.material = damagedMaterials[_currentMatIndex];
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
                ChangeShieldColor();
            }
        }

        private void ChangeShieldColor()
        {
            _currentMatIndex++;
            
            if (_currentMatIndex < damagedMaterials.Length)
            {
                meshRenderer.material = damagedMaterials[_currentMatIndex];
            }
        }
    }
}