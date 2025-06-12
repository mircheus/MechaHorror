using System;
using System.Collections;
using Hertzole.GoldPlayer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player
{
    public class EnergyModeSwitcher : MonoBehaviour
    {
        [Header("Settings:")] 
        [SerializeField] private MechEnergyMode defaultEnergyMode = MechEnergyMode.Radar;

        [Header("References: ")] 
        [SerializeField] private GoldPlayerController playerController;
        [SerializeField] private ShootingSystem shootingSystem;
        [SerializeField] private Dash dash;

        [Header("Input Actions: ")] 
        [SerializeField] private InputActionReference radarModeAction;
        [SerializeField] private InputActionReference movementModeAction;
        [SerializeField] private InputActionReference shooterModeAction;

        [Header("Movement Speeds: ")] 
        [SerializeField] private MovementSpeeds radarMovementSpeeds;
        [SerializeField] private MovementSpeeds movementSpeeds;
        [SerializeField] private MovementSpeeds shooterMovementSpeeds;

        private MovementSpeeds _currentMovementSpeeds;
        private MovementSpeeds _targetMovementSpeeds;
        private bool _isSwitchingModes = false;
        private Coroutine _fadeSpeedCoroutine;

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
            _currentMovementSpeeds = new MovementSpeeds(0, 0, 0);
            EnterDefaultMode();
        }

        private void Update()
        {
            // if (_isSwitchingModes)
            // {
            //     FadeSpeeds(_targetMovementSpeeds, 1f);
            // }
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
            switch (defaultEnergyMode)
            {
                case MechEnergyMode.Radar:
                    SwitchToRadarMode();
                    break;

                case MechEnergyMode.Movement:
                    SwitchToMovementMode();
                    break;

                case MechEnergyMode.Shooter:
                    SwitchToShooterMode();
                    break;
            }
        }

        private void SwitchToRadarMode()
        {
            OnRadarModeEvent?.Invoke();
            shootingSystem.SetShootingSystemActive(false, this);
            dash.SetDashActive(false, this);
            _isSwitchingModes = true;
            _targetMovementSpeeds = radarMovementSpeeds;
            FadeSpeeds(_targetMovementSpeeds, 1f);
        }

        private void SwitchToMovementMode()
        {
            OnMovementModeEvent?.Invoke();
            shootingSystem.SetShootingSystemActive(false, this);
            dash.SetDashActive(true, this);
            _isSwitchingModes = true;
            _targetMovementSpeeds = movementSpeeds;
            FadeSpeeds(_targetMovementSpeeds, 1f);
        }

        private void SwitchToShooterMode()
        {
            OnShooterModeEvent?.Invoke();
            shootingSystem.SetShootingSystemActive(true, this);
            dash.SetDashActive(false, this);
            _isSwitchingModes = true;
            _targetMovementSpeeds = shooterMovementSpeeds;
            FadeSpeeds(_targetMovementSpeeds, 1f);
        }

        private void FadeSpeeds(MovementSpeeds targetSpeeds, float duration)
        {
            if (_fadeSpeedCoroutine != null)
            {
                StopCoroutine(_fadeSpeedCoroutine);
                _fadeSpeedCoroutine = null;
            }

            _fadeSpeedCoroutine = StartCoroutine(FadeSpeedsCoroutine(targetSpeeds, duration));
        }

        private IEnumerator FadeSpeedsCoroutine(MovementSpeeds targetSpeeds, float duration)
        {
            float elapsedTime = 0f;
            MovementSpeeds initialSpeeds = _currentMovementSpeeds;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);

                _currentMovementSpeeds.ForwardSpeed = Mathf.Lerp(initialSpeeds.ForwardSpeed, targetSpeeds.ForwardSpeed, t);
                _currentMovementSpeeds.SidewaysSpeed = Mathf.Lerp(initialSpeeds.SidewaysSpeed, targetSpeeds.SidewaysSpeed, t);
                _currentMovementSpeeds.BackwardsSpeed = Mathf.Lerp(initialSpeeds.BackwardsSpeed, targetSpeeds.BackwardsSpeed, t);
                
                playerController.Movement.WalkingSpeeds = _currentMovementSpeeds;

                yield return null;
            }

            _isSwitchingModes = false;
        }

        internal enum MechEnergyMode
        {
            Radar,
            Movement,
            Shooter
        }
    }
}