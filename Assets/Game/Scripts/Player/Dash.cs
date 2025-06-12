using CameraShake;
using Hertzole.GoldPlayer;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player
{
    public class Dash : MonoBehaviour
    {
        [Header("Input: ")]
        [SerializeField] private InputActionReference dashAction;

        [Header("Dash Settings: ")]
        [SerializeField] private float dashForce = 20f;
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private float dashCooldown = 1f;
        
        [Header("Screen Shake Settings: ")]
        [Range(0.1f, 1f)]
        [SerializeField] private float screenShakeStrength = 1f;
        [SerializeField] private float frequency = 25f;
        [SerializeField] private int bouncesCount = 5;
        
        
        [Header("Dash Inertia: ")]
        [SerializeField] private AnimationCurve dashSpeedCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        
        [Header("References: ")]
        [SerializeField] private CharacterController controller;
        [SerializeField] private GoldPlayerController playerController;
        [SerializeField] private DashCollider dashCollider;
        [SerializeField] private ParticleSystem dashParticle;

        private bool _isDashing = false;
        private bool _isAbleToDash = true;
        private float _dashTime = 0f;
        private Vector3 _dashDirection;

        private void OnEnable()
        {
            dashAction.action.Enable();
            dashAction.action.performed += OnDash;
            dashCollider.DashTriggered += OnDashTriggered;
        }

        private void OnDisable()
        {
            dashAction.action.Disable();
            dashAction.action.performed -= OnDash;
            dashCollider.DashTriggered -= OnDashTriggered;
        }

        public void SetDashActive(bool isActive, EnergyModeSwitcher energyModeSwitcher)
        {
            _isAbleToDash = isActive;
        }

        private void OnDash(InputAction.CallbackContext context)
        {
            if (_isAbleToDash && !_isDashing)
            {
                StartDash();
            }
        }

        private void StartDash()
        {
            _isDashing = true;
            _isAbleToDash = false;
            _dashTime = dashDuration;

            // Choose dash direction (example: current forward)
            // dashDirection = transform.forward;
            var inputVector = playerController.Movement.GetInput(0f);
            _dashDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;
            _dashDirection = transform.TransformDirection(_dashDirection);
        }

        private void Update()
        {
            InertiaDash();
        }

        private void ResetDash()
        {
            _isAbleToDash = true;
        }

        private void InertiaDash()
        {
            if (_isDashing)
            {
                dashCollider.gameObject.SetActive(true);
                float t = 1f - (_dashTime / dashDuration); // 0 → 1 over dash duration
                float speedMultiplier = dashSpeedCurve.Evaluate(t);

                controller.Move(_dashDirection * dashForce * speedMultiplier * Time.deltaTime);

                _dashTime -= Time.deltaTime;
                if (_dashTime <= 0f)
                {
                    _isDashing = false;
                    dashCollider.gameObject.SetActive(false);
                    Invoke(nameof(ResetDash), dashCooldown);
                }
            }
        }

        private void StraightDash()
        {
            if (_isDashing)
            {
                controller.Move(_dashDirection * dashForce * Time.deltaTime);
                _dashTime -= Time.deltaTime;

                if (_dashTime <= 0f)
                {
                    _isDashing = false;
                    Invoke(nameof(ResetDash), dashCooldown);
                }
            }
        }

        private void OnDashTriggered()
        {
            dashParticle.Play();
            CameraShaker.Presets.ShortShake3D(screenShakeStrength, frequency, bouncesCount);
        }
    }
}