using System;
using System.Collections;
using CameraShake;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts
{
    public class ShootingSystem : MonoBehaviour
    {
        [SerializeField] private InputActionReference inputAction;
        [SerializeField] private GameObject[] projectiles;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private Animator handAnimator;
        [SerializeField] private float spawnOffset;
        [SerializeField] private float speed;
        [SerializeField] private float scaleMultiplier = 1f;
        [SerializeField] private float fireRate = 0.13f;
        
        [Header("Screen Shake Settings")]
        [Range(0.1f, 1f)]
        [SerializeField] private float screenShakeStrength = 1f;
        [SerializeField] private float frequency = 25f;
        [SerializeField] private int bouncesCount = 5;
        
        
        private int shoot = Animator.StringToHash("Shoot");

        private Vector3 Direction => spawnPosition.forward;
        private int currentProjectile = 0;
        private bool _isAbleToShoot;
        private Coroutine _fireRateCoroutine;

        private void OnEnable()
        {
            inputAction.action.Enable();
            inputAction.action.performed += OnShoot;
        }
        
        private void OnDisable()
        {
            inputAction.action.performed -= OnShoot;
            inputAction.action.Disable();
        }

        private void Start()
        {
            _isAbleToShoot = true;
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            // ShootProjectile();
            if (_isAbleToShoot)
            {
                if (_fireRateCoroutine != null)
                {
                    StopCoroutine(_fireRateCoroutine);
                }
                
                _fireRateCoroutine = StartCoroutine(Shoot());
                handAnimator.SetTrigger(shoot);
                CameraShaker.Presets.ShortShake3D(screenShakeStrength, frequency, bouncesCount); // Adjust the shake parameters as needed
            }
        }

        private IEnumerator Shoot()
        {
            _isAbleToShoot = false;
            ShootProjectile();
            yield return new WaitForSeconds(fireRate);
            _isAbleToShoot = true;
        }
        
        private void ShootProjectile()
        {
            Vector3 spawnPositionWithOffset = spawnPosition.position + Direction * spawnOffset;
            GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPositionWithOffset, Quaternion.identity);
            projectile.transform.localScale *= scaleMultiplier; // Scale the projectile
            projectile.transform.LookAt(spawnPositionWithOffset + Direction * 10f);
            projectile.GetComponent<Rigidbody>().AddForce(Direction * speed);
        }
    }
}