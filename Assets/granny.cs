using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class granny : MonoBehaviour
{
    public Elevator elevator;
    public VoiceLineManager voiceLineManager;
    public roomCheckForPlayer room;
    public GameStateManager gameState;

    public float grannySleepTime;
    private void Start()
    {

        AkSoundEngine.PostEvent("granny_sleep", gameObject);

        StartCoroutine(WaitForGrannyWakeUp());
    }

    private void Update()
    {
        if(room.playerInsideRoom)
        {
            voiceLineManager.PlayLevelVoiceline();
        }
    }
    private IEnumerator WaitForGrannyWakeUp()
    {
        yield return new WaitForSeconds(grannySleepTime);

        //AkSoundEngine.PostEvent("stop_granny_sleep", gameObject);

        AkSoundEngine.PostEvent("granny_wakeup", gameObject);

        StartCoroutine(WaitForElevator());
    }

    private IEnumerator WaitForElevator()
    {
        yield return new WaitForSeconds(30);

        voiceLineManager.PlayOutro();

        gameState.currentLevelComplete = true;
        elevator.Arrive();
    }

}
