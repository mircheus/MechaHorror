using System;
using System.Collections;
using CameraShake;
using Game.Scripts.Player;
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

        [Header("Weapon Heat Settings")] 
        [SerializeField] private GameObject[] overheatFXs;
        [SerializeField] private float fireRate = 0.13f;
        [SerializeField] private int maxHeat = 4;
        [SerializeField] private float timeToCoolDown = 5f;
        [SerializeField] private float timeToReset = 2f;
        
        [Header("Screen Shake Settings")]
        [Range(0.1f, 1f)]
        [SerializeField] private float screenShakeStrength = 1f;
        [SerializeField] private float frequency = 25f;
        [SerializeField] private int bouncesCount = 5;
        
        // private readonly int _shoot = Animator.StringToHash("Shoot");
        private readonly int _shoot = Animator.StringToHash("Mech_RightHand_Idle_Gun");
        
        private Vector3 Direction => spawnPosition.forward;
        private int currentProjectile = 0;
        private bool _isAbleToShoot;
        private Coroutine _fireRateCoroutine;
        private Coroutine _overheatCoroutine;
        private int _shotCounter = 0;
        private bool _isOverheated;

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
            _shotCounter = 0;
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            // ShootProjectile();
            if (_isAbleToShoot && _isOverheated == false)
            {
                if(_overheatCoroutine != null)
                {
                    StopCoroutine(_overheatCoroutine);
                }
                
                _overheatCoroutine = StartCoroutine(IncreaseWeaponHeat());
                
                if (_fireRateCoroutine != null)
                {
                    StopCoroutine(_fireRateCoroutine);
                }
                
                _fireRateCoroutine = StartCoroutine(Shoot());
                // handAnimator.SetTrigger(_shoot);
                handAnimator.Play(_shoot);
                CameraShaker.Presets.ShortShake3D(screenShakeStrength, frequency, bouncesCount); // Adjust the shake parameters as needed
            }
        }
        
        public void SetShootingSystemActive(bool isActive, EnergyModeSwitcher energyModeSwitcher)
        {
            _isAbleToShoot = isActive;
            Debug.Log("_isAbleToShoot: " + _isAbleToShoot);
        }

        private IEnumerator IncreaseWeaponHeat()
        {
            _shotCounter++;
            
            if (_shotCounter >= maxHeat)
            {
                _isOverheated = true;
                SetOverheatFX(true);
                yield return new WaitForSeconds(timeToCoolDown);
                SetOverheatFX(false);
                _isOverheated = false;
                _shotCounter = 0;
            }
            else
            {
                yield return new WaitForSeconds(timeToReset);
                _shotCounter = 0;
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

        private void SetOverheatFX(bool isActive)
        {
            foreach (var fx in overheatFXs)
            {
                fx.gameObject.SetActive(isActive);
            }
        }
    }
}