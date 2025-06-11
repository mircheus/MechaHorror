using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player
{
    public class EnergyModeSwitcher : MonoBehaviour
    {
        [SerializeField] private InputActionReference radarModeAction;
        [SerializeField] private InputActionReference movementModeAction;
        [SerializeField] private InputActionReference shooterModeAction;

        public event UnityAction OnRadarModeEvent;
        public event UnityAction OnMovementModeEvent;
        public event UnityAction OnShooterModeEvent;
        
        private void OnEnable()
        {
            radarModeAction.action.Enable();
            movementModeAction.action.Enable();
            shooterModeAction.action.Enable();
            
            radarModeAction.action.performed += OnRadarMode;
            movementModeAction.action.performed += OnMovementMode;
            shooterModeAction.action.performed += OnShooterMode;
        }

        private void OnDisable()
        {
            radarModeAction.action.Disable();
            movementModeAction.action.Disable();
            shooterModeAction.action.Disable();
        }

        private void OnRadarMode(InputAction.CallbackContext context)
        {
            OnRadarModeEvent?.Invoke();
            Debug.Log("Radar mode activated");
        }

        private void OnMovementMode(InputAction.CallbackContext context)
        {
            OnMovementModeEvent?.Invoke();
            Debug.Log("Movement mode activated");
        }

        private void OnShooterMode(InputAction.CallbackContext context)
        {
            OnShooterModeEvent?.Invoke();
            Debug.Log("Shooter mode activated");
        }
    }
}