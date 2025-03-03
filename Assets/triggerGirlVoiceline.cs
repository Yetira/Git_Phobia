using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerGirlVoiceline : MonoBehaviour
{
    public triggerRock triggerRock;
    
    public VoiceLineManager voiceLineManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerRock.voicelineCounter++;
            voiceLineManager.PlayLevelVoiceline();

            Debug.Log("Play Girl Voiceline");
        }
    }
}
