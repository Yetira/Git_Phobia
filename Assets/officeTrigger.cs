using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class officeTrigger : MonoBehaviour
{
    public VoiceLineManager voiceLineManager;
    public hall_enter hall_Enter;

    public GameObject windowCloseTrigger;

    public float officeEventDuration;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hall_Enter.voicelineCounter == 2)
        {
            hall_Enter.voicelineCounter++;
            voiceLineManager.PlayLevelVoiceline();

            //office events: play conversation event in people game obj, play door open sound, move portal obstruction, (move people inside? + close door?)

            StartCoroutine(WaitForOfficeEventEnd());

            Debug.Log("Play third Voiceline");        }
    }

    private IEnumerator WaitForOfficeEventEnd()
    {
        yield return new WaitForSeconds(officeEventDuration);

        voiceLineManager.PlayLevelVoiceline();
        Debug.Log("Office Event over. Play fourth voiceline");

        windowCloseTrigger.SetActive(true);
    }
}
