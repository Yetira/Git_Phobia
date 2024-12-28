using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public radio radio;

    public List<Vector3> targetPositions;

    private int currentTargetIndex = -1;

    private bool isProcessing = false;

    public void ChangeTargetPosition()
    {
        if (currentTargetIndex <= targetPositions.Count -1 && !isProcessing)
        {
            isProcessing = true;

            AkSoundEngine.PostEvent("Target_Hit", gameObject);
            StartCoroutine(HandleTargetDelay());
        }
    }

    private IEnumerator HandleTargetDelay()
    {
        yield return new WaitForSeconds(3);

        currentTargetIndex++;

        radio.StopPlay();
        
        radio.transform.localPosition = targetPositions[currentTargetIndex];
        radio.StartPlay();
        
        isProcessing = false;
    }
}
