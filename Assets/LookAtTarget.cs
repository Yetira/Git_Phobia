using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public radio radio;
    public AltFirstPersonController playerController;
    public MoveToTarget moveToTarget;

    public Camera playerCamera;

    public string targetTag;

    public float detectionRange;

    public List<Vector3> targetPositions;

    public float startDelay = 5.0f; // Delay before the tutorial starts, adjustable in the Inspector.

    private int currentTargetIndex = 0;

    private bool isProcessing = false;

    private bool lastLookedAt = false;

    public Collider targetCollider;

    private HashSet<int> processedTargets = new HashSet<int>();

    public float positionSwitchDelay;

    private bool tutorialActive = false; // Flag to control when the tutorial starts.

    private void Start()
    {
        currentTargetIndex = 0;
        radio.transform.localPosition = targetPositions[currentTargetIndex];

        // Start a coroutine to delay the tutorial logic.
        StartCoroutine(StartTutorialAfterDelay());
    }

    private IEnumerator StartTutorialAfterDelay()
    {
        Debug.Log("Waiting for the tutorial to start...");
        yield return new WaitForSeconds(startDelay);

        tutorialActive = true; // Enable the tutorial logic.
        Debug.Log("Tutorial started!");
    }

    void Update()
    {
        if (!tutorialActive || isProcessing) return; // Exit if tutorial hasn't started or is currently processing.

        Ray ray = new(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            if (hit.collider.CompareTag(targetTag) && !processedTargets.Contains(currentTargetIndex))
            {
                isProcessing = true;

                Debug.Log("Target in view!");

                processedTargets.Add(currentTargetIndex);

                if (targetCollider != null)
                    targetCollider.enabled = false;

                AkSoundEngine.PostEvent("Target_Hit", gameObject);

                StartCoroutine(HandleTargetDelay());
            }
        }

        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * detectionRange, Color.red);

        if (processedTargets.Contains(2) && !lastLookedAt)
        {
            lastLookedAt = true;

            Debug.Log("Player can move now!");
            playerController.canMove = true;
        }
    }

    private IEnumerator HandleTargetDelay()
    {
        yield return new WaitForSeconds(positionSwitchDelay);

        if (currentTargetIndex < targetPositions.Count - 1)
        {
            radio.StopPlay();
        }

        ChangeTargetPosition();

        if (targetCollider != null)
            targetCollider.enabled = true;

        isProcessing = false;
        Debug.Log("Delay complete. Target moved. Ready for next detection.");
    }

    private void ChangeTargetPosition()
    {
        if (currentTargetIndex < 2)
        {
            currentTargetIndex++;
            radio.transform.localPosition = targetPositions[currentTargetIndex];
            radio.StartPlay();
        }
    }
}
