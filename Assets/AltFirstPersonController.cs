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

    public GameStateManager gameStateManager;

    public GameObject Feet;

    private CharacterController characterController;
    private PlayerInputActions playerInputActions;

    private Vector2 moveInput;

    private float m_StepCycle = 0f;         // Tracks the current step cycle progress
    private float m_NextStep = 0f;          // Threshold for the next footstep sound
    public float m_StepInterval = 0.5f;     // Time interval between footsteps
    public float m_FixedSpeedFactor = 1.0f; // Factor to control footstep timing rate

    public bool canMove;

    void Awake()
    {
        canMove = false;

        // Initialize the input actions and register the Move action
        playerInputActions = new PlayerInputActions();

        playerInputActions.Player.Move.performed += OnMovePerformed;
        playerInputActions.Player.Move.canceled += OnMoveCanceled;
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

        if (gameStateManager.currentLevelIndex == 3 && !gameStateManager.insideLift)
        {
            AkSoundEngine.PostEvent("breath_slow", gameObject);
        }

        if (vrCamera == null)
        {
            Debug.LogError("VR Camera is not assigned. Please assign the VR camera (usually the Main Camera).");
        }
    }

    void Update()
    {
        // Handle movement
        Move();

        // Update step cycle for footstep sounds
        ProgressStepCycle();

        /*
        if(gameStateManager.currentLevelIndex > 3)
        {
            AkSoundEngine.PostEvent("stop_breathing", gameObject);
        }
        */
    }

    private void ProgressStepCycle()
    {
        // Check if character is moving
        if (moveInput.sqrMagnitude > 0 && characterController.velocity.sqrMagnitude > 0)
        {
            // Increment step cycle by a fixed value (e.g., based on desired steps per second)
            m_StepCycle += Time.fixedDeltaTime * m_FixedSpeedFactor; // Set m_FixedSpeedFactor based on desired timing

            // Check if it's time to play a footstep
            if (m_StepCycle > m_NextStep)
            {
                m_NextStep = m_StepCycle + m_StepInterval;

                PlayFootStepAudio();
            }
        }
        else
        {
            // Reset cycle when stop moving
            m_StepCycle = 0f;
            m_NextStep = m_StepInterval;
        }
    }

    private void PlayFootStepAudio()
    {
        AkSoundEngine.PostEvent("footsteps", Feet);
    }

    private void Move()
    {
        if (!canMove) return;

        // Use the VR camera's forward direction for movement alignment
        Vector3 forward = vrCamera.forward;
        Vector3 right = vrCamera.right;

        // Flatten the vectors to prevent vertical movement
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Compute the movement direction based on the left stick input
        Vector3 moveDirection = (forward * moveInput.y + right * moveInput.x) * movementSpeed * Time.deltaTime;

        // Apply the movement to the character controller
        characterController.Move(moveDirection);
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        // Read the movement vector from the input system
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Reset the movement vector when the input is released
        moveInput = Vector2.zero;
    }
}
