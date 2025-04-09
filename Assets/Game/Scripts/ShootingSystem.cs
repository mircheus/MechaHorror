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
        
        private int shoot = Animator.StringToHash("Shoot");

        private Vector3 Direction => spawnPosition.forward;
        private int currentProjectile = 0;

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
        
        public void OnShoot(InputAction.CallbackContext context)
        {
            ShootProjectile();
            handAnimator.SetTrigger(shoot);
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