using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public radio radio;
    public Elevator elevator;

    public List<Vector3> targetPositions;

    private int currentTargetIndex = -1;

    private bool isProcessing = false;

    public void ChangeTargetPosition()
    {
        if (isProcessing) return;

        if (currentTargetIndex < targetPositions.Count - 1)
        {
            isProcessing = true;

            AkSoundEngine.PostEvent("Target_Hit", gameObject);
            StartCoroutine(HandleTargetDelay());
        }
        else if (currentTargetIndex == targetPositions.Count - 1)
        {
            AkSoundEngine.PostEvent("Target_Hit", gameObject);
            radio.StopPlay();

            Debug.Log("Final target reached, triggering elevator.");
            elevator.Arrive();
        }
    }

    private IEnumerator HandleTargetDelay()
    {
        yield return new WaitForSeconds(3);

        if (currentTargetIndex < targetPositions.Count - 1)
        {
            currentTargetIndex++;

            radio.StopPlay();

            radio.transform.localPosition = targetPositions[currentTargetIndex];
            radio.StartPlay();
        }
        else
        {
            Debug.Log("All targets completed");
        }

        isProcessing = false; 
    }
}