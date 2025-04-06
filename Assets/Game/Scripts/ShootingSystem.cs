using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts
{
    public class ShootingSystem : MonoBehaviour
    {
        [SerializeField] private InputActionReference inputAction;
        
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
            Debug.Log("Shooting action triggered!");
        }
    }
}