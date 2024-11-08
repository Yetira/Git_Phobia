using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AltFirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 2.0f; // Control the movement speed of the player

    [Header("References")]
    public Transform vrCamera; // Reference to the VR Camera (usually the player's head)

    private CharacterController characterController;
    private PlayerInputActions playerInputActions;
    private bool isMovingForward = false;

    void Awake()
    {
        // Initialize the input actions and register the MoveForward action
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.MoveForward.performed += OnMoveForwardPerformed;
        playerInputActions.Player.MoveForward.canceled += OnMoveForwardCanceled;
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
        if (isMovingForward)
        {
            MoveForward();
        }
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
}
