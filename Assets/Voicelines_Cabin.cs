using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voicelines_Cabin : MonoBehaviour
{
    public float voiceLineDelay;

    public roomCheckForPlayer room;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (room.playerInsideRoom)
        {
            room.playerInsideRoom = false;
            StartCoroutine(VoicelineSequence());
        }
    }
    private IEnumerator VoicelineSequence()
    {
        yield return new WaitForSeconds(voiceLineDelay);

        AkSoundEngine.PostEvent("During_Level", gameObject);
    }

    /*private IEnumerator WaitForOutro()
    {
        //yield return new WaitForSeconds(voiceLineDelay);

       //AkSoundEngine.PostEvent("Level_Intro", gameObject);
    }
    */

    public void PlayIntro()
    {
        AkSoundEngine.PostEvent("Level_Intro", gameObject);
    }
}
