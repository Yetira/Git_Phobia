using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hall_enter : MonoBehaviour
{ 
    public VoiceLineManager voiceLineManager;

    public int voicelineCounter;

    private void Start()
    {
        voicelineCounter = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && voicelineCounter == 0)
        {
            voicelineCounter++;
            voiceLineManager.PlayLevelVoiceline();

            Debug.Log("Play First Voiceline");
        }
    }
}
