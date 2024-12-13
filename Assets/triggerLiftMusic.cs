using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerLiftMusic : MonoBehaviour
{
    public int playerLayer;

    public GameObject liftIntercom;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            AkSoundEngine.PostEvent("lift_music", liftIntercom);
        }
    }
}
