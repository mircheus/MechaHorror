using Game.Scripts.Enemies.MiniBoss.States;
using Game.Scripts.Interfaces;
using Game.Scripts.Utilities;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class Projectile : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private GameObject impactParticle;
        [SerializeField] private GameObject projectileParticle;

        private Rigidbody _rigidbody;
        private Transform _myTransform;

        private float _destroyTimer = 0f;
        private bool _destroyed = false;
        private GameObject _impactProjectile;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _myTransform = transform;

            projectileParticle = Instantiate(projectileParticle, _myTransform.position, _myTransform.rotation) as GameObject;
            projectileParticle.transform.parent = _myTransform;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_destroyed)
            {
                return;
            }
            
            if(collision.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(1);
                HitObject(collision.GetContact(0).normal);
                return;
            }

            HitObject(collision.GetContact(0).normal);
        }

        private void FixedUpdate()
        {
            _destroyTimer += Time.deltaTime;
            
            if (_destroyTimer >= 5f)
            {
                DestroyMissile();
            }
        }

        public void HitShield(Shield shield)
        {
            HitObject(shield.transform.forward);
        }
        
        public void HitBall(Ball ball)
        {
            HitObject(Vector3.up);
        }

        private void DestroyMissile()
        {
            _destroyed = true;
            Destroy(projectileParticle, 3f);
            Destroy(gameObject);
        }

        private void RotateTowardsDirection()
        {
            if (_rigidbody.linearVelocity != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.linearVelocity.normalized, Vector3.up);
                float angle = Vector3.Angle(_myTransform.forward, _rigidbody.linearVelocity.normalized);
                float lerpFactor = angle * Time.deltaTime; // Use the angle as the interpolation factor
                _myTransform.rotation = Quaternion.Slerp(_myTransform.rotation, targetRotation, lerpFactor);
            }
        }

        private void HitObject(RaycastHit hit)
        {
            _impactProjectile = Instantiate(impactParticle, _myTransform.position, Quaternion.FromToRotation(Vector3.up, hit.normal));
            Destroy(projectileParticle, 3f);
            Destroy(_impactProjectile, 5.0f);
            DestroyMissile();
        }

        private void HitObject(Vector3 normal)
        {
            _impactProjectile = Instantiate(impactParticle, _myTransform.position, Quaternion.FromToRotation(Vector3.up, normal));
            Destroy(projectileParticle, 3f);
            Destroy(_impactProjectile, 5.0f);
            DestroyMissile();
        }
    }
}