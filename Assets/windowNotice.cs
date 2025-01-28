using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windowNotice : MonoBehaviour
{
    public VoiceLineManager voiceLineManager;
    public hall_enter hall_Enter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hall_Enter.voicelineCounter == 1)
        {
            hall_Enter.voicelineCounter++;
            voiceLineManager.PlayLevelVoiceline();

            Debug.Log("Play second Voiceline");
        }
    }
}
