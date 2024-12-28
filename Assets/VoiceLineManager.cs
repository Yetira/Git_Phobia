using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineManager : MonoBehaviour
{
    public float voiceLineDelay;

    public roomCheckForPlayer room;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(room.playerInsideRoom)
        {
            room.playerInsideRoom = false;
            StartCoroutine(WaitForIntro());
        }
    }
    private IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds(voiceLineDelay);

        AkSoundEngine.PostEvent("Level_Intro", gameObject);
    }

    /*private IEnumerator WaitForOutro()
    {
        //yield return new WaitForSeconds(voiceLineDelay);

       //AkSoundEngine.PostEvent("Level_Intro", gameObject);
    }
    */
}
