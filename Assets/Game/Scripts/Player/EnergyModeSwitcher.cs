using System;
using Hertzole.GoldPlayer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player
{
    public class EnergyModeSwitcher : MonoBehaviour
    {
        [Header("Settings:")]
        [SerializeField] private MechMode defaultMode = MechMode.Radar;
        
        [Header("References: ")]
        [SerializeField] private GoldPlayerController playerController;
        [SerializeField] private ShootingSystem shootingSystem;
        
        [Header("Input Actions: ")]
        [SerializeField] private InputActionReference radarModeAction;
        [SerializeField] private InputActionReference movementModeAction;
        [SerializeField] private InputActionReference shooterModeAction;
        
        [Header("Movement Speeds: ")]
        [SerializeField] private MovementSpeeds radarMovementSpeeds;
        [SerializeField] private MovementSpeeds movementSpeeds;
        [SerializeField] private MovementSpeeds shooterMovementSpeeds;

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
            
            radarModeAction.action.performed -= OnRadarMode;
            movementModeAction.action.performed -= OnMovementMode;
            shooterModeAction.action.performed -= OnShooterMode;
        }

        private void Start()
        {
            EnterDefaultMode();
        }

        private void OnRadarMode(InputAction.CallbackContext context)
        {
            SwitchToRadarMode();
        }

        private void OnMovementMode(InputAction.CallbackContext context)
        {
            SwitchToMovementMode();
        }

        private void OnShooterMode(InputAction.CallbackContext context)
        {
            SwitchToShooterMode();
        }
        
        private void EnterDefaultMode()
        {
            switch (defaultMode)
            {
                case MechMode.Radar:
                    SwitchToRadarMode();
                    break;
                
                case MechMode.Movement:
                    SwitchToMovementMode();
                    break;
                
                case MechMode.Shooter:
                    SwitchToShooterMode();
                    break;
            }
        }
        
        private void SwitchToRadarMode()
        {
            OnRadarModeEvent?.Invoke();
            shootingSystem.SetShootingSystemActive(false, this);
            playerController.Movement.WalkingSpeeds = radarMovementSpeeds;
        }
        
        private void SwitchToMovementMode()
        {
            OnMovementModeEvent?.Invoke();
            shootingSystem.SetShootingSystemActive(false, this);
            playerController.Movement.WalkingSpeeds = movementSpeeds;
        }
        
        private void SwitchToShooterMode()
        {
            OnShooterModeEvent?.Invoke();
            shootingSystem.SetShootingSystemActive(true, this);
            playerController.Movement.WalkingSpeeds = shooterMovementSpeeds;
        }
    }
    
    internal enum MechMode
    {
        Radar, 
        Movement,
        Shooter
    }
}