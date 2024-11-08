using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AltFirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 2.0f; // Control the movement speed of the player
    public float rotationSpeed = 100.0f; // Control the rotation speed of the player

    [Header("References")]
    public Transform vrCamera; // Reference to the VR Camera (usually the player's head)

    private CharacterController characterController;
    private PlayerInputActions playerInputActions;
    private bool isMovingForward = false;
    private Vector2 lookInput;

    void Awake()
    {
        // Initialize the input actions and register the MoveForward and Look actions
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.MoveForward.performed += OnMoveForwardPerformed;
        playerInputActions.Player.MoveForward.canceled += OnMoveForwardCanceled;

        // Register the Look action for the left stick
        playerInputActions.Player.Look.performed += OnLookPerformed;
        playerInputActions.Player.Look.canceled += OnLookCanceled;
    }

    void OnEnable()
    {
        // Enable the input action map
        playerInputActions.Player.Enable();
    }

    void OnDisable()
    {
        // Disable the input action map
        playerInputActions.Player.Disable();
    }

    void Start()
    {
        // Get the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();

        if (vrCamera == null)
        {
            Debug.LogError("VR Camera is not assigned. Please assign the VR camera (usually the Main Camera).");
        }
    }

    void Update()
    {
        // Handle movement
        if (isMovingForward)
        {
            MoveForward();
        }

        // Handle rotation based on left stick input
        RotatePlayer();
    }

    private void MoveForward()
    {
        // Calculate the forward movement vector based on the VR Camera's forward direction
        Vector3 forwardDirection = vrCamera.forward;
        forwardDirection.y = 0; // Prevent vertical movement

        // Move the player in the forward direction
        Vector3 movement = forwardDirection.normalized * movementSpeed * Time.deltaTime;
        characterController.Move(movement);
    }

    private void RotatePlayer()
    {
        // Only rotate if there is look input (from the left stick)
        if (lookInput.sqrMagnitude > 0.01f)
        {
            // Rotate the player based on the horizontal input
            float horizontalRotation = lookInput.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(0, horizontalRotation, 0);
        }
    }

    private void OnMoveForwardPerformed(InputAction.CallbackContext context)
    {
        // Called when the "A" button is pressed
        isMovingForward = true;
    }

    private void OnMoveForwardCanceled(InputAction.CallbackContext context)
    {
        // Called when the "A" button is released
        isMovingForward = false;
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        // Called when the left stick is moved
        lookInput = context.ReadValue<Vector2>();
    }

    private void OnLookCanceled(InputAction.CallbackContext context)
    {
        // Called when the left stick is released
        lookInput = Vector2.zero;
    }
}
