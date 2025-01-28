using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineManager : MonoBehaviour
{
    private void Start()
    {
        PlayIntro();
    }
    public void PlayIntro()
    {
        AkSoundEngine.PostEvent("Level_Intro", gameObject);
    }

    public void PlayOutro()
    {
        AkSoundEngine.PostEvent("Level_Outro", gameObject);
    }

    public void PlayLevelVoiceline()
    {
        AkSoundEngine.PostEvent("During_Level", gameObject);
    }
}
