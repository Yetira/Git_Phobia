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

    public GameObject Feet;

    private CharacterController characterController;
    private PlayerInputActions playerInputActions;
    private Vector2 moveInput;
    private Vector2 lookInput;

    private float m_StepCycle = 0f;         // Tracks the current step cycle progress
    private float m_NextStep = 0f;          // Threshold for the next footstep sound
    public float m_StepInterval = 0.5f;     // Time interval between footsteps
    public float m_FixedSpeedFactor = 1.0f; // Factor to control footstep timing rate

    void Awake()
    {
        // Initialize the input actions and register the MoveForward and Look actions
        playerInputActions = new PlayerInputActions();
        
        playerInputActions.Player.Move.performed += OnMovePerformed;
        playerInputActions.Player.Move.canceled += OnMoveCanceled;

       
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
        Move();

        // Handle rotation based on left stick input
        RotatePlayer();

        ProgressStepCycle();
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
            //reset cycle when stop moving
            m_StepCycle = 0f;
            m_NextStep = m_StepInterval;
        }
    }

    private void PlayFootStepAudio()
    {
      
        AkSoundEngine.PostEvent("footstep_event", Feet);

    }
    private void Move()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= movementSpeed * Time.deltaTime;
        
        characterController.Move(moveDirection);
        
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

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Called when the "A" button is released
        moveInput = Vector2.zero;
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
