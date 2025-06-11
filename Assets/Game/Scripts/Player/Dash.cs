using Hertzole.GoldPlayer;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player
{
    public class Dash : MonoBehaviour
    {
        [Header("Input")]
        [SerializeField] private InputActionReference dashAction;

        [Header("Dash Settings")]
        [SerializeField] private float dashForce = 20f;
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private float dashCooldown = 1f;
        
        [Header("Dash Inertia")]
        [SerializeField] private AnimationCurve dashSpeedCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);


        [Header("References")]
        [SerializeField] private CharacterController controller;
        [SerializeField] private Transform cameraHeadTransform;
        [SerializeField] private GoldPlayerController playerController;

        private bool isDashing = false;
        private bool canDash = true;
        private float dashTime = 0f;
        private MovementSpeeds _originalSpeeds;

        private Vector3 dashDirection;

        private void OnEnable()
        {
            _originalSpeeds = playerController.Movement.WalkingSpeeds;
            dashAction.action.performed += OnDash;
            dashAction.action.Enable();
        }

        private void OnDisable()
        {
            dashAction.action.performed -= OnDash;
            dashAction.action.Disable();
        }

        private void OnDash(InputAction.CallbackContext context)
        {
            if (canDash && !isDashing)
            {
                StartDash();
            }
        }

        private void StartDash()
        {
            isDashing = true;
            canDash = false;
            dashTime = dashDuration;

            // Choose dash direction (example: current forward)
            // dashDirection = transform.forward;
            var inputVector = playerController.Movement.GetInput(0f);
            dashDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;
            dashDirection = transform.TransformDirection(dashDirection);
        }

        private void Update()
        {
            InertiaDash();
        }

        private void ResetDash()
        {
            canDash = true;
        }

        private void InertiaDash()
        {
            if (isDashing)
            {
                float t = 1f - (dashTime / dashDuration); // 0 → 1 over dash duration
                float speedMultiplier = dashSpeedCurve.Evaluate(t);

                controller.Move(dashDirection * dashForce * speedMultiplier * Time.deltaTime);

                dashTime -= Time.deltaTime;
                if (dashTime <= 0f)
                {
                    isDashing = false;
                    Invoke(nameof(ResetDash), dashCooldown);
                }
            }
        }

        private void StraightDash()
        {
            if (isDashing)
            {
                controller.Move(dashDirection * dashForce * Time.deltaTime);
                dashTime -= Time.deltaTime;

                if (dashTime <= 0f)
                {
                    isDashing = false;
                    Invoke(nameof(ResetDash), dashCooldown);
                }
            }
        }
    }
}