using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Camera playerCamera;

    public string targetTag;

    public float detectionRange;

    public GameObject[] targets;

    public float switchDelay;

    private int currentTargetIndex = 0;

    private bool isProcessing = false;

    void Update()
    {
        Ray ray = new(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, detectionRange))
        {
            if(hit.collider.CompareTag(targetTag) && !isProcessing)
            {
                Debug.Log("Target in view!");

                AkSoundEngine.PostEvent("Target_Hit", gameObject);

                StartCoroutine(SwitchTargetWithDelay());
            }
        }

        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * detectionRange, Color.red);

        
    }

    private IEnumerator SwitchTargetWithDelay()
    {
        isProcessing = true;

        yield return new WaitForSeconds(switchDelay);

        targets[currentTargetIndex].SetActive(false);

        currentTargetIndex++;
        if(currentTargetIndex < targets.Length)
        {
            targets[currentTargetIndex].SetActive(true);
        }

        isProcessing = false;

    }
}
