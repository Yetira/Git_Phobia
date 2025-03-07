using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radio : MonoBehaviour
{
    public MoveToTarget moveToTarget;

    private int enableCounter = 0; 
    public float initialDelay = 3; 

    private void OnEnable()
    {
        enableCounter++; 

        if (enableCounter == 1)
        {
            StartCoroutine(DelayedStartPlay(initialDelay));
        }
        else
        {
            StartPlay();
        }
    }

    private IEnumerator DelayedStartPlay(float delay)
    {
        yield return new WaitForSeconds(delay); 
        StartPlay();
    }

    public void StartPlay()
    {
        AkSoundEngine.PostEvent("Play_radio", gameObject);
    }

    public void StopPlay()
    {
        AkSoundEngine.PostEvent("Stop_radio", gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered radio collider.");

            // Always call ChangeTargetPosition to handle both intermediate and final targets
            moveToTarget.ChangeTargetPosition();
        }
    }
}
