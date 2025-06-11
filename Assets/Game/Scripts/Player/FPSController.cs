using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float gravity = -9.81f;
    // [SerializeField] private Transform cameraHolder; // Assign CameraHolder in Inspector
    
    private float _verticalRotation = 0f;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private float _verticalVelocity;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleMouseLook();
    }

    private void HandleMovement()
    {
        // Get input direction
        Vector3 moveDirection = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        moveDirection *= moveSpeed;

        // Apply gravity
        if (controller.isGrounded)
        {
            if (_verticalVelocity < 0) _verticalVelocity = -2f; // Small downward force
        }
        else
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }

        // Apply final movement
        Vector3 velocity = moveDirection + Vector3.up * _verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleMouseLook()
    {
        float mouseX = _lookInput.x * mouseSensitivity;
        float mouseY = _lookInput.y * mouseSensitivity;

        // Rotate player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera vertically (limit rotation to avoid flipping)
        _verticalRotation -= mouseY;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -90f, 90f);

        cameraHolder.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
    }

    // Input System Callbacks
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
