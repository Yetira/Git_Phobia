using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windowNotice : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //play next voiceline (Oh, looks like someone left the window open)
        }
    }
}
