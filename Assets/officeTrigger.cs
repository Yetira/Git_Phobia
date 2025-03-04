using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class officeTrigger : MonoBehaviour
{
    public VoiceLineManager voiceLineManager;
    public hall_enter hall_Enter;

    public GameObject windowCloseTrigger;

    public float officeEventDuration;

    private bool hasProcessed;

    private void Start()
    {
        hasProcessed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hall_Enter.voicelineCounter == 2 && !hasProcessed)
        {
            hasProcessed = true;
            
            //office events: play conversation event in people game obj

            StartCoroutine(WaitForOfficeEventEnd());

        }
    }

    private IEnumerator WaitForOfficeEventEnd()
    {
        yield return new WaitForSeconds(officeEventDuration);

        voiceLineManager.PlayLevelVoiceline();
        Debug.Log("Office Event over. Play fourth voiceline");

        windowCloseTrigger.SetActive(true);
    }
}
