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

    private int currentTargetIndex = 0;

    private bool isProcessing = false;

    private bool lastLookedAt = false;

    public Collider targetCollider;

    private HashSet<int> processedTargets = new HashSet<int>(); // Track processed targets

    public float positionSwitchDelay; // Delay before moving the target


    private void Start()
    {
        currentTargetIndex = 0;
        radio.transform.localPosition = targetPositions[currentTargetIndex];
    }

    void Update()
    {
        if (isProcessing) return;

        Ray ray = new(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            if (hit.collider.CompareTag(targetTag) && !processedTargets.Contains(currentTargetIndex))
            {
                isProcessing = true;

                Debug.Log("Target in view!");

                processedTargets.Add(currentTargetIndex); // Mark as processed

                if (targetCollider != null)
                    targetCollider.enabled = false;

                AkSoundEngine.PostEvent("Target_Hit", gameObject);

                // Start a Coroutine for the delay before moving the target
                StartCoroutine(HandleTargetDelay());
            }
        }

        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * detectionRange, Color.red);

        if(processedTargets.Contains(2) && !lastLookedAt)
        {
            lastLookedAt = true;

            Debug.Log("Player can move now!");
            playerController.canMove = true;
        }
    }

    private IEnumerator HandleTargetDelay()
    {
        // Wait for the specified delay before moving the target
        yield return new WaitForSeconds(positionSwitchDelay);

        radio.StopPlay();

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
