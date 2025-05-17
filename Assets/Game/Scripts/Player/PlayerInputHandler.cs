using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] private InputActionReference radarModeAction;
        [SerializeField] private InputActionReference movementModeAction;
        [SerializeField] private InputActionReference shooterModeAction;

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
            Debug.Log("Radar mode activated");
        }

        private void OnMovementMode(InputAction.CallbackContext obj)
        {
            Debug.Log("Movement mode activated");
        }

        private void OnShooterMode(InputAction.CallbackContext obj)
        {
            Debug.Log("Shooter mode activated");
        }
    }
}