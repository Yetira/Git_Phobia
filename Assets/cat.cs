using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat : MonoBehaviour
{
    private uint playingID; 

    private void Start()
    {
        playingID = AkSoundEngine.PostEvent("cat", gameObject);
        AkSoundEngine.SetSwitch("cat_mode", "meow", gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AkSoundEngine.StopPlayingID(playingID);

            AkSoundEngine.SetSwitch("cat_mode", "purr", gameObject);

            playingID = AkSoundEngine.PostEvent("cat", gameObject);

            Debug.Log("Player in cat zone: Purring starts");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AkSoundEngine.StopPlayingID(playingID);

            AkSoundEngine.SetSwitch("cat_mode", "meow", gameObject);

            playingID = AkSoundEngine.PostEvent("cat", gameObject);

            Debug.Log("Player out of cat zone: Meowing starts");
        }
    }
}
