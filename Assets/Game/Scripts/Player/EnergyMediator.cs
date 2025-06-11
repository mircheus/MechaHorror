using System;
using Hertzole.GoldPlayer;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Player
{
    public class EnergyMediator : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private EnergyModeSwitcher energyModeSwitcher;

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

        private void OnRadarMode()
        {
            OnRadarModeEvent?.Invoke();
        }

        private void OnMovementMode()
        {
            OnMovementModeEvent?.Invoke();
        }

        private void OnShooterMode()
        {
            OnShooterModeEvent?.Invoke();
        }
    }
}