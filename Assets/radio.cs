using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radio : MonoBehaviour
{
    public MoveToTarget moveToTarget;

    private void Start()
    {
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
