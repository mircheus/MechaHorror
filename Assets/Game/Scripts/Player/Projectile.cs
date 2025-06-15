using UnityEngine;
using System.Collections;
using Game.Scripts;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.BaseEnemy;
using Game.Scripts.Enemies.MiniBoss.States;

namespace RetroArsenal
{
    public class Projectile : MonoBehaviour
    {
        public GameObject impactParticle;
        public GameObject projectileParticle;
        public GameObject muzzleParticle;
        public GameObject[] trailParticles;
        [Header("Adjust if not using Sphere Collider")]
        public float colliderRadius = 1f;
        [Range(0f, 1f)]
        public float collideOffset = 0.15f;

        private Rigidbody rb;
        private Transform myTransform;
        private SphereCollider sphereCollider;

        private float destroyTimer = 0f;
        private bool destroyed = false;
        private GameObject _impactProjectile;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            myTransform = transform;
            sphereCollider = GetComponent<SphereCollider>();

            projectileParticle = Instantiate(projectileParticle, myTransform.position, myTransform.rotation) as GameObject;
            projectileParticle.transform.parent = myTransform;

            if (muzzleParticle)
            {
                muzzleParticle = Instantiate(muzzleParticle, myTransform.position, myTransform.rotation) as GameObject;

                Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
            }
        }
		
        void FixedUpdate()
        {
            if (destroyed)
            {
                return;
            }

            float rad = sphereCollider ? sphereCollider.radius : colliderRadius;

            Vector3 dir = rb.linearVelocity;
            float dist = dir.magnitude * Time.deltaTime;

            if (rb.useGravity)
            {
                // Handle gravity separately to correctly calculate the direction.
                dir += Physics.gravity * Time.deltaTime;
                dist = dir.magnitude * Time.deltaTime;
            }

            RaycastHit hit;
            if (Physics.SphereCast(myTransform.position, rad, dir, out hit, dist))
            {
                myTransform.position = hit.point + (hit.normal * collideOffset);
                
                if (hit.transform.tag == "Target") // TODO: Избавить от тэгов
                {
                    Target retroTarget = hit.transform.GetComponent<Target>();
                    if (retroTarget != null)
                    {
                        retroTarget.OnHit();
                    }
                }

                if (hit.collider.isTrigger == false)
                {
                    if(hit.collider.TryGetComponent(out BaseEnemy enemy))
                    {
                        enemy.TakeDamage(1);
                        HitObject(hit);
                    }
                }
                
                // Damage numbers popup
                if (hit.transform.CompareTag("Enemy")) // TODO: Избавить от тэгов
                {
                    DamagePopUpGenerator.current.CreatePopUpDefault(hit.transform.position);
                    Debug.Log("Hit Enemy");
                    HitObject(hit);
                }

                // foreach (GameObject trail in trailParticles)
                // {
                //     GameObject curTrail = myTransform.Find(projectileParticle.name + "/" + trail.name).gameObject;
                //     curTrail.transform.parent = null;
                //     Destroy(curTrail, 3f);
                // }
            }
            else
            {
                // Increment the destroyTimer if the projectile hasn't hit anything.
                destroyTimer += Time.deltaTime;

                // Destroy the missile if the destroyTimer exceeds 5 seconds.
                if (destroyTimer >= 5f)
                {
                    DestroyMissile();
                }
            }

            RotateTowardsDirection();
        }

        public void HitShield(Shield shield)
        {
            HitObject(shield.transform.forward);
        }

        private void DestroyMissile()
        {
            destroyed = true;

            foreach (GameObject trail in trailParticles)
            {
                GameObject curTrail = myTransform.Find(projectileParticle.name + "/" + trail.name).gameObject;
                curTrail.transform.parent = null;
                Destroy(curTrail, 3f);
            }
            Destroy(projectileParticle, 3f);
            Destroy(gameObject);

            ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();
            //Component at [0] is that of the parent i.e. this object (if there is any)
            for (int i = 1; i < trails.Length; i++)
            {
                ParticleSystem trail = trails[i];
                if (trail.gameObject.name.Contains("Trail"))
                {
                    trail.transform.SetParent(null);
                    Destroy(trail.gameObject, 2f);
                }
            }
        }

        private void RotateTowardsDirection()
        {
            if (rb.linearVelocity != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(rb.linearVelocity.normalized, Vector3.up);
                float angle = Vector3.Angle(myTransform.forward, rb.linearVelocity.normalized);
                float lerpFactor = angle * Time.deltaTime; // Use the angle as the interpolation factor
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, lerpFactor);
            }
        }

        private void HitObject(RaycastHit hit)
        {
            _impactProjectile = Instantiate(impactParticle, myTransform.position, Quaternion.FromToRotation(Vector3.up, hit.normal));
            Destroy(projectileParticle, 3f);
            Destroy(_impactProjectile, 5.0f);
            DestroyMissile();
        }

        private void HitObject(Vector3 normal)
        {
            _impactProjectile = Instantiate(impactParticle, myTransform.position, Quaternion.FromToRotation(Vector3.up, normal));
            Destroy(projectileParticle, 3f);
            Destroy(_impactProjectile, 5.0f);
            DestroyMissile();
        }
    }
}