using System;
using Hertzole.GoldPlayer;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Player
{
    public class EnergyMediator : MonoBehaviour
    {
        [Header("Settings:")]
        [SerializeField] private MechMode defaultMode = MechMode.Radar;
        [Header("References: ")]
        [SerializeField] private EnergyModeSwitcher energyModeSwitcher;
        [SerializeField] private EnergyPanelUI energyPanelUI;
        [SerializeField] private GoldPlayerController playerController;
        [Header("Movement Mode: ")]
        [SerializeField] private MovementSpeeds movementSpeeds;
        [SerializeField] private MovementSpeeds radarMovementSpeeds;
        [SerializeField] private MovementSpeeds shooterMovementSpeeds;

        public event UnityAction OnRadarModeEvent;
        public event UnityAction OnMovementModeEvent;
        public event UnityAction OnShooterModeEvent;

        private void OnEnable()
        {
            energyModeSwitcher.OnRadarModeEvent += OnRadarMode;
            energyModeSwitcher.OnMovementModeEvent += OnMovementMode;
            energyModeSwitcher.OnShooterModeEvent += OnShooterMode;
        }

        private void OnDisable()
        {
            energyModeSwitcher.OnRadarModeEvent -= OnRadarMode;
            energyModeSwitcher.OnMovementModeEvent -= OnMovementMode;
            energyModeSwitcher.OnShooterModeEvent -= OnShooterMode;
        }

        private void Start()
        {
            EnterDefaultMode();
        }

        private void OnRadarMode()
        {
            SwitchModes();
            playerController.Movement.WalkingSpeeds = radarMovementSpeeds;
            OnRadarModeEvent?.Invoke();
        }

        private void OnMovementMode()
        {
            SwitchModes();
            OnMovementModeEvent?.Invoke();
            playerController.Movement.WalkingSpeeds = movementSpeeds;
        }

        private void OnShooterMode()
        {
            SwitchModes();
            OnShooterModeEvent?.Invoke();
        }

        private void SwitchModes()
        {
            playerController.Movement.WalkingSpeeds = shooterMovementSpeeds;
        }

        private void EnterDefaultMode()
        {
            switch (defaultMode)
            {
                case MechMode.Radar:
                    OnRadarMode();
                    break;
                
                case MechMode.Movement:
                    OnMovementMode();
                    break;
                
                case MechMode.Shooter:
                    OnShooterMode();
                    break;
            }
        }
    }

    internal enum MechMode
    {
        Radar, 
        Movement,
        Shooter
    }
}