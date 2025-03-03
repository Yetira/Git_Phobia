using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerEndVoiceline : MonoBehaviour
{
    public VoiceLineManager voiceLineManager;

    private bool isProcessing;

    private void Start()
    {
        isProcessing = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isProcessing)
        {
            isProcessing=true;
            
            voiceLineManager.PlayOutro();

            Debug.Log("Play Girl Voiceline");
        }
    }
}
