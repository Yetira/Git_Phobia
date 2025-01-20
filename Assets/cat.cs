using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cat : MonoBehaviour
{
    private void Start()
    {
        AkSoundEngine.SetSwitch("cat_mode","purr", gameObject);

        AkSoundEngine.PostEvent("cat", gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AkSoundEngine.SetSwitch("cat_mode", "purr", gameObject);

            Debug.Log("player in cat zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AkSoundEngine.SetSwitch("cat_mode", "meow", gameObject);
        }
    }
}
