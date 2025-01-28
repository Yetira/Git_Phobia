using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerRock : MonoBehaviour
{
    public rockFall rock;
    public VoiceLineManager voiceLineManager;

    public int playerLayer;

    public int voicelineCounter;

    public float rockFallDelay;

    private void Start()
    {
        voicelineCounter = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            if(voicelineCounter == 0)
            {
                voicelineCounter++;
                voiceLineManager.PlayLevelVoiceline();
            }
            
            StartCoroutine(DelayRock());
        }
    }

    private IEnumerator DelayRock()
    {
        yield return new WaitForSeconds(rockFallDelay);

        rock.RockFall();

        voiceLineManager.PlayLevelVoiceline();

        Debug.Log("Play voiceline 2 (after rock)");
    }
}
