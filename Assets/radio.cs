using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartPlay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartPlay()
    {
        AkSoundEngine.PostEvent("Play_radio", gameObject);
    }
    public void StopPlay()
    {
        AkSoundEngine.PostEvent("Stop_radio", gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AkSoundEngine.PostEvent("Target_Hit", gameObject);
        }
    }
}
