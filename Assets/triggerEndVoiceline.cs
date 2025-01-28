using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerEndVoiceline : MonoBehaviour
{
    public VoiceLineManager voiceLineManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            voiceLineManager.PlayOutro();

            Debug.Log("Play Girl Voiceline");
        }
    }
}
